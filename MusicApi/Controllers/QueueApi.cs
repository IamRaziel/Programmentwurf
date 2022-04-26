using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicApi.Backend;
using MusicApi.Backend.Music;
using System.Collections.Generic;

namespace MusicApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QueueApi : ControllerBase
    {

        [HttpPost]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public IActionResult Post()
        {
            //TODO
            return Ok();
        }

        [HttpGet]
        [Route("get")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<ITrack>))]
        public IActionResult GetQueue()
        {
            return Ok(BackendController.QueueController.GetTracksOfQueue());
        }

        [HttpPost]
        [Route("add-tracks")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public IActionResult AddTracksToQueue(IList<string> ids)
        {
            BackendController.QueueController.AddTracksToQueue(ids);
            return Ok(true);
        }

        [HttpPost]
        [Route("add-albums")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public IActionResult AddAlbumsToQueue(IList<string> ids)
        {
            BackendController.QueueController.AddAlbumsToQueue(ids);
            return Ok(true);
        }

        [HttpPost]
        [Route("add-playlists")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public IActionResult AddPlaylistsToQueue(IList<string> ids)
        {
            BackendController.QueueController.AddPlaylistsToQueue(ids);
            return Ok(true);
        }

        [HttpDelete]
        [Route("remove-tracks")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public IActionResult RemoveTracksFromQueue(ISet<int> positions)
        {
            BackendController.QueueController.RemoveTracksFromQueue(positions);
            return Ok(true);
        }


        [HttpPost]
        [Route("change-mode")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(QueueMusicPlayMode))]
        public IActionResult ChangeMode()
        {
            return Ok(BackendController.QueueController.SwitchModeOfQueue());
        }

        [HttpPut]
        [Route("start")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public IActionResult StartPlayer()
        {
            BackendController.QueueController.PlayNextTrackInQueue();
            return Ok(true);
        }

        [HttpPut]
        [Route("stop")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public IActionResult StopPlayer()
        {
            BackendController.QueueController.StopMusicPlayer();
            return Ok(true);
        }

        [HttpPut]
        [Route("resume")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public IActionResult ResumePlayer()
        {
            BackendController.ResumeMusicPlayer();
            return Ok(true);
        }

        [HttpPut]
        [Route("move")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<ITrack>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult MoveTrackInQueue(string id, int position)
        {
            if (BackendController.MoveTracksInQueue(id, position))
                return Ok(BackendController.GetTracksOfQueue());
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
