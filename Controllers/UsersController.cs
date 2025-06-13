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
        SignInManager<UserInfoModel> signInManager)
        : Controller
    {
        [HttpPost("auth")]
        public async Task<IActionResult> SendUserPhone([FromQuery] string phoneNumber)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

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

            var userInfo = await repository.GetByPhone(verifyCode.PhoneNumber);
            var jsonResult = new JsonResult(userInfo);
            if (userInfo == null)
            {
                userInfo = await repository.Add(verifyCode.PhoneNumber);
                jsonResult = new JsonResult(userInfo)
                {
                    StatusCode = StatusCodes.Status201Created
                };
                if (userInfo == null) return BadRequest("Не удалось создать нового пользователя");
            }

            await signInManager.SignInAsync(userInfo, false);

            // var identityRole = await roleManager.FindByNameAsync(UserInfoModel.ADMIN_ROLE);
            // if (identityRole == null)
            // {
            //     var result = await roleManager.CreateAsync(new IdentityRole(UserInfoModel.ADMIN_ROLE));
            //     if (!result.Succeeded) return BadRequest("Ошибка создания роли /n" + result.Errors);
            // }
            //
            // await userManager.AddToRoleAsync(user, UserInfoModel.ADMIN_ROLE);

            return jsonResult;
        }

        [HttpGet]
        // [Authorize(Roles = AuthUserModel.ADMIN_ROLE)] TODO добавить когда модель юзера перетекёт в БД
        public async Task<IActionResult> GetAllUsers()
        {
            var userInfoModels = await repository.GetAll();
            var jsonResult = Json(userInfoModels);

            if (!userInfoModels.Any())
                jsonResult.StatusCode = StatusCodes.Status204NoContent;

            return jsonResult;
        }
        
        [HttpGet("{phoneNumber}")]
        // [Authorize(Roles = AuthUserModel.ADMIN_ROLE)]
        public async Task<IActionResult> GetUserByPhoneNumber([FromRoute] string phoneNumber)
        {
            var userInfoModel = await repository.GetByPhone(phoneNumber);

            if (userInfoModel == null)
                return NotFound();

            return Json(userInfoModel);
        }

        [HttpPut]
        public async Task<IActionResult> PutUserInfo([FromBody] PutUserInfoModelDto userInfoModelDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var userInfoModel = await repository.Put(userInfoModelDto);

            return userInfoModel != null ? Json(userInfoModel) : NotFound();
        }
        
        [HttpPatch]
        public async Task<IActionResult> PatchUserInfo([FromBody] PatchUserInfoModelDto userInfoModelDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var userInfoModel = await repository.Patch(userInfoModelDto);

            return userInfoModel != null ? Json(userInfoModel) : NotFound();
        }

        [HttpDelete("{phoneNumber}")]
        public async Task<IActionResult> DeleteUserByPhoneNumber([FromRoute] string phoneNumber)
        {
            var userInfoModel = await repository.DeleteByPhone(phoneNumber);

            return userInfoModel != null ? Json(userInfoModel) : NotFound();
        }
    }
}

