using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace Avoska.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : Controller
    {
        [HttpPost]
        public IActionResult SendUserPhone([FromBody] UserInfo userInfo)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var result = Json(userInfo);
            result.StatusCode = StatusCodes.Status501NotImplemented;
            
            return result;
        }

        [HttpPost("{id}")]
        public IActionResult SendAuthCode(int id)
        {
            Response.StatusCode = StatusCodes.Status501NotImplemented;

            return Json(new { code = id });
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            return StatusCode(StatusCodes.Status501NotImplemented);
        }
        
        [HttpGet("{userId}")]
        public IActionResult GetUserById(int userId)
        {
            var result = Json(new {id = userId});
            result.StatusCode = StatusCodes.Status501NotImplemented;
            return result;
        }
    }

    public record UserInfo(string PhoneNumber);
}

