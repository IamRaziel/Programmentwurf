using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicApi.Backend;
using MusicApi.Model;

namespace MusicApi.Controllers
{
    [ApiController]
    [Route("api/track")]
    public class TrackApi : ControllerBase
    {
        //"11dFghVXANMlKmJXsNCbNl"

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DTOTrack))]
        public IActionResult GetTrack(string id)
        {
            return Ok(BackendController.TrackController.GetTrackFromUrl(id));
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public IActionResult DeleteTrack(string id)
        {
            return Ok(BackendController.TrackController.DeleteTrack(id));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public IActionResult UploadTrack(IFormFile file)
        {
            return Ok(BackendController.TrackController.UploadTrack(file));
        }
    }
}
