using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicApi.Backend;
using MusicApi.Model;

namespace MusicApi.Controllers
{
    [ApiController]
    [Route("api/playlist/tracks")]
    public class PlaylistTrackApi : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<DTOTrack>))]
        public IActionResult GetTracksOfPlaylist(string id)
        {
            return Ok(BackendController.GetTracksOfPlaylist(id));
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public IActionResult RemoveTrackOfPlaylist(string playlistID, string trackID)
        {
            return Ok(BackendController.RemoveTrackFromPlaylist(playlistID, trackID));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public IActionResult AddTrackToPlaylist(string playlistID, string trackID)
        {
            return Ok(BackendController.AddTrackToPlaylist(playlistID, trackID));
        }
    }
}
