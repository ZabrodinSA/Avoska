using Avoska.Models.Users;
using Avoska.Repositories.Users;
using Avoska.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Avoska.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController(
        IUsersInfoRepository repository,
        IPhoneAuthService phoneAuthService,
        SignInManager<AuthUserModel> signInManager,
        UserManager<AuthUserModel> userManager,
        RoleManager<IdentityRole> roleManager)
        : Controller
    {
        [HttpPost("auth")]
        public async Task<IActionResult> SendUserPhone([FromQuery] string phoneNumber)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var user = await userManager.FindByNameAsync(phoneNumber);
            if (user == null)
            {
                user = new AuthUserModel
                {
                    UserName = phoneNumber,
                    PhoneNumber = phoneNumber
                };
                var result = await userManager.CreateAsync(user);
                if (!result.Succeeded) return BadRequest(result.Errors);
            }

            phoneAuthService.SendAuthCode(phoneNumber);
            
            return Ok($"Код отправлен на номер: {phoneNumber}");
        }

        [HttpPost("codeCheck")]
        public async Task<IActionResult> SendAuthCode([FromBody] VerifyCodeModelDto verifyCode)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var isValid = await phoneAuthService.VerifyCodeAsync(verifyCode);
            if (!isValid) return BadRequest($"Неверный код аунтификации, номер: {verifyCode.PhoneNumber}");

            var user = await userManager.FindByNameAsync(verifyCode.PhoneNumber);
            if (user == null)
                return BadRequest($"Нет подключений с номером {verifyCode.PhoneNumber}");

            await signInManager.SignInAsync(user, false);

            var identityRole = await roleManager.FindByNameAsync(AuthUserModel.ADMIN_ROLE);
            if (identityRole == null)
            {
                var result = await roleManager.CreateAsync(new IdentityRole(AuthUserModel.ADMIN_ROLE));
                if (!result.Succeeded) return BadRequest("Ошибка создания роли /n" + result.Errors);
            }
            
            await userManager.AddToRoleAsync(user, AuthUserModel.ADMIN_ROLE);
            var infoModelFromBd = repository.GetByPhone(verifyCode.PhoneNumber);
            var jsonResult = Json(infoModelFromBd);
            if (infoModelFromBd == null)
            {
                jsonResult.Value = repository.Add(verifyCode.PhoneNumber);
                jsonResult.StatusCode = StatusCodes.Status201Created;
            }

            return jsonResult;
        }

        [HttpGet]
        // [Authorize(Roles = AuthUserModel.ADMIN_ROLE)] TODO добавить когда модель юзера перетекёт в БД
        public IActionResult GetAllUsers()
        {
            var userInfoModels = repository.GetAll();
            var jsonResult = Json(userInfoModels);

            if (!userInfoModels.Any())
                jsonResult.StatusCode = StatusCodes.Status204NoContent;

            return jsonResult;
        }
        
        [HttpGet("{id:guid}")]
        // [Authorize(Roles = AuthUserModel.ADMIN_ROLE)]
        public IActionResult GetUserById([FromRoute] Guid id)
        {
            var userInfoModel = repository.GetById(id);

            if (userInfoModel == null)
                return NotFound();

            return Json(userInfoModel);
        }

        [HttpPut]
        public IActionResult PutUserInfo([FromBody] PutUserInfoModelDto userInfoModelDto)
        {
            var userInfoModel = repository.Put(userInfoModelDto);

            if (userInfoModel == null)
                return NotFound();

            return Json(userInfoModel);
        }
        
        [HttpPatch]
        public IActionResult PatchUserInfo([FromBody] PatchUserInfoModelDto userInfoModelDto)
        {
            var userInfoModel = repository.Patch(userInfoModelDto);

            if (userInfoModel == null)
                return NotFound();

            return Json(userInfoModel);
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteUser([FromRoute] Guid id)
        {
            var userInfoModel = repository.DeleteById(id);

            if (userInfoModel == null)
                return NotFound();

            return Json(userInfoModel);
        }
    }
}

