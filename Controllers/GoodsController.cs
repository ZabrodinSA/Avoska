using Avoska.Models.Goods;
using Avoska.Repositories.Goods;
using Microsoft.AspNetCore.Mvc;

namespace Avoska.Controllers;

[ApiController]
[Route("[controller]")]
public class GoodsController(IGoodsInfoRepository repository) : Controller
{
    [HttpPost]
    // [Authorize(AuthUserModel.ADMIN_ROLE)]
    public async Task<IActionResult> AddGood([FromBody] AddGoodInfoModelDto addGoodInfoModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var goodInfoModel = await repository.GetByName(addGoodInfoModel.Name);
        if (goodInfoModel != null)
        {
            return BadRequest($"Имя продукта [{addGoodInfoModel.Name}] уже занято");
        }
        
        goodInfoModel = await repository.Add(addGoodInfoModel);
        if (goodInfoModel != null)
        {
            var jsonResult = Json(goodInfoModel);
            jsonResult.StatusCode = StatusCodes.Status201Created;
            return jsonResult;
        }

        return BadRequest($"Не получилось добавить продукт: {addGoodInfoModel.Name}");
    }

    [HttpGet]
    public async Task<IActionResult> GetAllGoods()
    {
        var goodInfoModels = await repository.GetAll();
        var jsonResult = Json(goodInfoModels);

        if (!goodInfoModels.Any())
            jsonResult.StatusCode = StatusCodes.Status204NoContent;

        return jsonResult;
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteGoodById([FromRoute] Guid id)
    {
        var goodInfoModel = await repository.DeleteById(id);

        if (goodInfoModel == null)
            return NotFound();

        return Json(goodInfoModel);
    }

    [HttpPut]
    public async Task<IActionResult> PutGood([FromBody] PutGoodInfoModelDto goodInfoModelDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var goodInfoModel = await repository.Put(goodInfoModelDto);

        return goodInfoModel != null ? Json(goodInfoModel) : NotFound();
    }

    [HttpPatch]
    public async Task<IActionResult> PatchGood([FromBody] PatchGoodInfoModelDto goodInfoModelDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var goodInfoModel = await repository.Patch(goodInfoModelDto);

        return goodInfoModel != null ? Json(goodInfoModel) : NotFound();
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetGoodById([FromRoute] Guid id)
    {
        var goodInfoModel = await repository.GetById(id);

        return goodInfoModel != null ? Json(goodInfoModel) : NotFound();
    }

    [HttpGet("searchByCategory")]
    public async Task<IActionResult> GetGoodsByCategoryName([FromQuery] string categoryName)
    {
        var goodInfoModels = await repository.GetByCategoryName(categoryName);
        var jsonResult = Json(goodInfoModels);

        if (!goodInfoModels.Any())
            jsonResult.StatusCode = StatusCodes.Status204NoContent;
            
        return jsonResult;
    }
    
    [HttpGet("byName")]
    public async Task<IActionResult> GetGoodByName([FromQuery] string name)
    {
        var goodInfoModel = await repository.GetByName(name);
        
        return goodInfoModel != null ? Json(goodInfoModel) : NotFound();
    }
}
