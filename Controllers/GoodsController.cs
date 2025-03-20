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
    public IActionResult AddGood([FromBody] AddGoodInfoModelDto addGoodInfoModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var jsonResult = Json(repository.Add(addGoodInfoModel));
        jsonResult.StatusCode = StatusCodes.Status201Created;
        
        return jsonResult;
    }

    [HttpGet]
    public IActionResult GetAllGoods()
    {
        var goodInfoModels = repository.GetAll();
        var jsonResult = Json(goodInfoModels);

        if (!goodInfoModels.Any())
            jsonResult.StatusCode = StatusCodes.Status204NoContent;

        return jsonResult;
    }
    
    [HttpDelete("{id:guid}")]
    public IActionResult DeleteGoodById([FromRoute] Guid id)
    {
        var goodInfoModel = repository.DeleteById(id);

        if (goodInfoModel == null)
            return NotFound();

        return Json(goodInfoModel);
    }

    [HttpPut]
    public IActionResult PutGood([FromBody] PutGoodInfoModelDto goodInfoModelDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var goodInfoModel = repository.Put(goodInfoModelDto);

        if (goodInfoModel == null)
            return NotFound();

        return Json(goodInfoModel);
    }

    [HttpPatch]
    public IActionResult PatchGood([FromBody] PatchGoodInfoModelDto goodInfoModelDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var goodInfoModel = repository.Patch(goodInfoModelDto);

        if (goodInfoModel == null)
            return NotFound();

        return Json(goodInfoModel);
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetGoodById([FromRoute] Guid id)
    {
        var goodInfoModel = repository.GetById(id);

        if (goodInfoModel == null)
            return NotFound();
        
        return Json(goodInfoModel);
    }

    [HttpGet("searchByCategory")]
    public IActionResult GetGoodsByCategoryName([FromQuery] string categoryName)
    {
        var goodInfoModels = repository.GetByCategoryName(categoryName);
        var jsonResult = Json(goodInfoModels);

        if (!goodInfoModels.Any())
            jsonResult.StatusCode = StatusCodes.Status204NoContent;
            
        return jsonResult;
    }
    
    [HttpGet("searchByName")]
    public IActionResult SearchGoodsByName([FromQuery] string name)
    {
        var goodInfoModel = repository.GetByName(name);

        if (goodInfoModel == null)
            return NotFound();
        
        return Json(goodInfoModel);
    }
}
