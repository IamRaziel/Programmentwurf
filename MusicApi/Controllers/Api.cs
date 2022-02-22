using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MusicApi.Controllers
{
    public class Api
    {
        [ApiController]
        [Route("[controller]")]
        public class WeatherForecastController : ControllerBase
        {

            [HttpGet]
            [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<string>))]

            public IActionResult Get()
            {
                return Ok(new string[] { "Hallo", "Welt", "!" });
            }
        }
    }
}
