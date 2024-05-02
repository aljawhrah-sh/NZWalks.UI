using System;
using NZWalks.UI.Models;
using NZWalks.UI.Models.DTO;

namespace NZWalks.UI.Services
{
	public class NZWalksTypedClientService
	{

		public readonly HttpClient client;

		public NZWalksTypedClientService(HttpClient client)
		{
			client.BaseAddress = new Uri("https://localhost:7100/api/");
			this.client = client;

        }

		public async Task<List<RegionDto>> GetAllRegionsAsync()
		{
			List<RegionDto> regions = new List<RegionDto>();
			try
			{
                var httpResponse = await client.GetFromJsonAsync<List<RegionDto>>("regions");
                regions.AddRange(httpResponse);
                return regions;

            }catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}


        }

		public async Task<RegionDto?> GetRegionAsync(Guid id)
		{
			try
			{
				var region = await client.GetFromJsonAsync<RegionDto>($"regions/{id.ToString()}");
				return region;

			}catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
			

		}

		public async Task<RegionDto?> EditRegionAsync(Guid id, RegionDto region)
		{
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


        public async Task<HttpResponseMessage> AddRegionAsync(AddRegionViewModel region)
		{
			try
			{
                var httpResponse = await client.PostAsJsonAsync("regions", region);
                httpResponse.EnsureSuccessStatusCode();
                return httpResponse;

            }catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}


		}

		public async Task<HttpResponseMessage> DeleteRegionAsync(Guid id)
		{
			try
			{
                var httpResponse = await client.DeleteAsync($"regions/{id}");
                httpResponse.EnsureSuccessStatusCode();
                return httpResponse;

            }catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}

		}
    }
}

