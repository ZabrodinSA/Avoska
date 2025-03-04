using Microsoft.AspNetCore.Mvc;

namespace Avoska.Controllers;

[ApiController]
[Route("[controller]")]
public class GoodsController : Controller
{
    [HttpPost]
    public IActionResult AddGood([FromBody] AddGoodModel addGoodModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        Response.StatusCode = StatusCodes.Status501NotImplemented;
        
        return Json(addGoodModel);
    }
    
    [HttpDelete("{id}")]
    public IActionResult DeleteGoodById(int id)
    {
        Response.StatusCode = StatusCodes.Status501NotImplemented;

        return Json(new { deletedId = id });
    }

    [HttpPut]
    public IActionResult UpdateGood([FromBody] AddGoodModel addGoodModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        Response.StatusCode = StatusCodes.Status501NotImplemented;
        
        return Json(addGoodModel);
    }

    [HttpPatch]
    public IActionResult PatchGood([FromBody] AddGoodModel addGoodModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        Response.StatusCode = StatusCodes.Status501NotImplemented;
        
        return Json(addGoodModel);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetGoodById(int id)
    {
        Response.StatusCode = StatusCodes.Status501NotImplemented;

        return Json(new { goodId = id });
    }

    [HttpGet]
    public IActionResult GetGoodByCategory([FromQuery] string categoryId)
    {
        Response.StatusCode = StatusCodes.Status501NotImplemented;

        return Json(new { category = categoryId });
    }
    
    [HttpGet("search")]
    public IActionResult SearchGoodsByName([FromQuery] string search)
    {
        var httpRequest = Request;
        Response.StatusCode = StatusCodes.Status501NotImplemented;

        return Json(new { searchParam =  search});
    }
}

public record NutritionalValue(
    string Proteins,
    string Fat,
    string Carbohydrates
);

public record AddGoodModel(
    string CategoryId,
    string Name,
    string Description,
    string Image,
    string Composition,
    string Weight,
    NutritionalValue NutritionalValue,
    string ManufactureCountry,
    DateTime ExpirationDate
);