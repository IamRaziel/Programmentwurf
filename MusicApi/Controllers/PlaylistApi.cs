using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicApi.Backend;
using MusicApi.Model;

namespace MusicApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PlaylistApi : ControllerBase
    {
        //"3cEYpjA9oz9GiPac4AsH4n"

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DTOPlaylist))]
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public IActionResult UpdatePlaylist(string id, string name, string image)
        {
            return Ok(BackendController.UpdatePlaylist(id, name, image));
        }
    }
}
