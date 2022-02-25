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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IAlbum))]
        public IActionResult Get()
        {
            //return Ok(BackendController.GetTrack("11dFghVXANMlKmJXsNCbNl"));
            return Ok(BackendController.GetAlbum("4aawyAB9vmqN3uQ7FjRGTy"));
        }
    }
}
