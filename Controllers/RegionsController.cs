using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NZWalks.UI.Models;
using NZWalks.UI.Models.DTO;
using NZWalks.UI.Services;
using NZWalksMVC.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NZWalks.UI.Controllers
{
    public class RegionsController : Controller
    {

        private readonly IHttpClientFactory httpClientFactory;
        //private readonly NZWalksService service;
        private readonly NZWalksTypedClientService service;

        public RegionsController(IHttpClientFactory httpClientFactory, NZWalksTypedClientService service) 
        {
            this.httpClientFactory = httpClientFactory;
            this.service = service;

        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<RegionDto> response = new List<RegionDto>();
            response = await service.GetAllRegionsAsync();
            ViewBag.Response = response;
            return View(response);

        }


        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddRegionViewModel model)
        {
            var response = await service.AddRegionAsync(model);

            if (response is not null)
            {
                return RedirectToAction("Index", "Regions");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {

            var region = await service.GetRegionAsync(id);
            return View(region);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, RegionDto region)
        {

            var response = await service.EditRegionAsync(id, region);
            if (response is not null)
            {
                return RedirectToAction("Edit", "Regions");
            }

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Delete(RegionDto request)
        {
            try
            {


                var httpResponseMessage = await service.DeleteRegionAsync(request.Id);

                return RedirectToAction("Index", "Regions");

            }
            catch (Exception ex)
            {

            }
            return View("Edit");
        }



    }
}

