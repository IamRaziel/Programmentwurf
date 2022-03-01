using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicApi.Backend;
using MusicApi.Backend.Music;

namespace MusicApi.Controllers
{
    [ApiController]
    [Route("api/track")]
    public class TrackApi : ControllerBase
    {
        //"11dFghVXANMlKmJXsNCbNl"

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ITrack))]
        public IActionResult GetTrack(string id)
        {
            return Ok(BackendController.GetTrack(id));
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public IActionResult DeleteTrack(string id)
        {
            return Ok(BackendController.DeleteTrack(id));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public IActionResult UploadAudioFile()
        {
            return Ok(true);
        }
    }
}
