using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicApi.Backend;
using MusicApi.Backend.Music;

namespace MusicApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PlaylistApi : ControllerBase
    {
        //"3cEYpjA9oz9GiPac4AsH4n"

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IPlaylist))]
        public IActionResult GetPlaylist(string id)
        {
            return Ok(BackendController.GetPlaylist(id));
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public IActionResult DeletePlaylist(string id)
        {
            return Ok(BackendController.DeletePlaylist(id));
        }
    }
}
