using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicApi.Backend;
using MusicApi.Backend.Music;



namespace MusicApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Api : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ITrack))]
        public IActionResult Get()
        {
            return Ok(BackendController.GetTrack("11dFghVXANMlKmJXsNCbNl"));
        }
    }
}
