using Meteorites.Business.Models;
using Meteorites.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace Meteorites.Web.Controllers
{
    [ApiController]
    [Route("api/meteorites")]
    public class MeteoriteController(MeteoriteService meteoriteService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetMeteorites([FromQuery] Filter filter, OrderingOptions orderingOptions)
        {
            try
            {
                var meteorites = meteoriteService.GetMeteoritesData(filter, orderingOptions);

                return Ok(meteorites);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected Controller Error: {ex.Message}");

                return StatusCode(500);
            }
           
        }
    }
}
