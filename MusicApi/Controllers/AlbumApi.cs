using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicApi.Backend;
using MusicApi.Model;

namespace MusicApi.Controllers
{
    [ApiController]
    [Route("api/album")]
    public class AlbumApi : ControllerBase
    {
        //"4aawyAB9vmqN3uQ7FjRGTy"

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DTOAlbum))]
        public IActionResult GetAlbum(string id)
        {
            return Ok(BackendController.GetAlbum(id));
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public IActionResult DeleteAlbum(string id)
        {
            return Ok(BackendController.DeleteAlbum(id));
        }
    }
}
