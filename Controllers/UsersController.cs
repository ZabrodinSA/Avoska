using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace Avoska.Controllers
{
    public class UsersController : Controller
    {
        [HttpPost]
        public async Task<ActionResult> SendUserPhone()
        {
            // Убедись, что запрос содержит тело
            if (!Request.Body.CanRead)
            {
                return BadRequest("Request body is empty.");
            }

            string? userPhone = null;
            // Чтение тела запроса
            using (var reader = new StreamReader(Request.Body))
            {
                string json = await reader.ReadToEndAsync();

                // Десериализация JSON в объект
                var myObject = JsonSerializer.Deserialize<MyModel>(json);

                if (myObject != null) userPhone = myObject.phoneNumber;
            }

            return Json(new { success = true, phone = userPhone });
        }

        [HttpPost]
        public ActionResult SendAuthCode(int id)
        {
            return Json(new { code = id });
        }
    }

    class MyModel
    {
        public string? phoneNumber { get; set; }
    }
}

