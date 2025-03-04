using Microsoft.AspNetCore.Mvc;

namespace Avoska.Controllers;

[ApiController]
[Route("[controller]")]
public class CatalogsController : Controller
{
    [HttpPost("[action]")]
    public IActionResult AddCategory([FromBody] AddCategoryModel addCategoryModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        Response.StatusCode = StatusCodes.Status501NotImplemented;
        
        return Json(addCategoryModel);
    }

    [HttpGet]
    public IActionResult GetCatalog()
    {
        return StatusCode(StatusCodes.Status501NotImplemented);
    }
}

public record AddCategoryModel(string Name, string Image);