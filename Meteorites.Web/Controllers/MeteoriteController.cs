using Meteorites.Business.Models;
using Meteorites.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace Meteorites.Web.Controllers
{
    [ApiController]
    [Route("api")]
    public class MeteoriteController(IMeteoriteService meteoriteService) : ControllerBase
    {
        [HttpGet("meteorites")]
        public async Task<ActionResult<IReadOnlyList<MeteoriteCompositionData>>> GetMeteorites([FromQuery] Filter filter, [FromQuery]OrderingOptions orderingOptions)
        {
            try
            {
                var meteorites = await meteoriteService.GetMeteoritesData(filter, orderingOptions);

                return Ok(meteorites);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected Controller Error: {ex.Message}");

                return StatusCode(500);
            }
           
        }

        [HttpGet("rec-classes")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetRecClasses()
        {
            try
            {
                var classes = await meteoriteService.GetRecClasses();

                return Ok(classes);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected Controller Error: {ex.Message}");

                return StatusCode(500);
            }

        }

        [HttpGet("years")]
        public async Task<ActionResult<IReadOnlyList<int>>> GetYears()
        {
            try
            {
                var years = await meteoriteService.GetYears();

                return Ok(years);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected Controller Error: {ex.Message}");

                return StatusCode(500);
            }

        }
    }
}
