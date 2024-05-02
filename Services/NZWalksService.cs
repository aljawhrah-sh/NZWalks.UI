using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NZWalks.UI.Models;
using NZWalks.UI.Models.DTO;

namespace NZWalks.UI.Services
{
    //Named clients
    public class NZWalksService
    {
        private readonly IHttpClientFactory _factory;


        public NZWalksService(IHttpClientFactory factory)
        {
            this._factory = factory;
        }

        public async Task<RegionDto?> GetRegionAsync(Guid id)
        {
            var client =  _factory.CreateClient("NZWalks");
            var region = await client.GetFromJsonAsync<RegionDto>($"regions/{id.ToString()}");


            if(region is not null)
            {
                return region;
            }

            return null;
        }

        public async Task<List<RegionDto>> GetAllRegionsAsync()
        {
            List<RegionDto> response = new List<RegionDto>();
            try
            {

                var client = _factory.CreateClient("NZWalks");

                var httpResponseMessage = await client.GetFromJsonAsync<List<RegionDto>>("regions");

                response.AddRange(httpResponseMessage);

                return response;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<HttpResponseMessage> AddRegionAsync(AddRegionViewModel region)
        {
            try
            {
                var client = _factory.CreateClient("NZWalks");
                var response = await client.PostAsJsonAsync("regions", region);
                return response.EnsureSuccessStatusCode();
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<HttpResponseMessage> DeleteRegionAsync(Guid id)
        {

            var client = _factory.CreateClient("NZWalks");
            try
            {
                var response = await client.DeleteAsync($"regions/{id}");
                return response.EnsureSuccessStatusCode();

            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }   
        
            public async Task<RegionDto?> EditRegionAsync(Guid id, RegionDto region)
            {
                var client = _factory.CreateClient("NZWalks");
                try
                {
               
                    var httpresponse = await client.PutAsJsonAsync($"regions/{id}", region);
                    httpresponse.EnsureSuccessStatusCode();
                    var response = await httpresponse.Content.ReadFromJsonAsync<RegionDto?>();

                    return response;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

        }
    }
}
