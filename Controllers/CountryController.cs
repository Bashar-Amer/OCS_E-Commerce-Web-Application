using CampTravelGear.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CampTravelGear.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {

        [HttpGet("GetCountries")]
        public async Task<IActionResult> GetCountries([FromServices] IMemoryCache cache, [FromServices] IHttpClientFactory clientFactory)
        {
            if (!cache.TryGetValue("CountryList", out List<CountryData>? countries))
            {
                var client = clientFactory.CreateClient();

                var response = await client.GetFromJsonAsync<CountryApiResponse>("https://countriesnow.space/api/v0.1/countries");

                if (response == null || response.Error)
                {
                    return StatusCode(500, "Failed to fetch countries from external API.");
                }

                countries = response.Data;

                cache.Set("CountryList", countries, TimeSpan.FromDays(1));
            }

            return Ok(countries);
        }
    }
}
