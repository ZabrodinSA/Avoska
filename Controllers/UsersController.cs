using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace Avoska.Controllers
{
    public class UsersController : Controller
    {
        [HttpPost]
        public async Task<ActionResult> SendUserPhone()
        {
            if (!Request.Body.CanRead)
            {
                return BadRequest("Request body is empty.");
            }

            UserInfo? userInfo;
            using (var reader = new StreamReader(Request.Body))
            {
                string json = await reader.ReadToEndAsync();
                userInfo = JsonSerializer.Deserialize<UserInfo>(json);
            }

            if (userInfo == null) return new BadRequestResult();
            
            return Json(userInfo);
        }

        [HttpPost]
        public ActionResult SendAuthCode(int id)
        {
            return Json(new { code = id });
        }
    }

    record UserInfo(string phoneNumber);
}

