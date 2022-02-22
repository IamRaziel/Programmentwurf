using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicApi.Backend;
using System.Collections.Generic;



namespace MusicApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Api : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<string>))]
        public IActionResult Get()
        {
            return Ok(new string[] { BackendController.GetTrack() });
        }
    }
}
