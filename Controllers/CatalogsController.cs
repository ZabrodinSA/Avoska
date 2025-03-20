using Avoska.Models.Catalogs;
using Avoska.Repositories.Catalogs;
using Microsoft.AspNetCore.Mvc;

namespace Avoska.Controllers;

[ApiController]
[Route("[controller]")]
public class CatalogsController(ICategoriesRepository repository) : Controller
{
    [HttpPost("[action]")]
    // [Authorize(Roles = AuthUserModel.ADMIN_ROLE)]
    public IActionResult AddCategory([FromBody] AddCategoryModelDto addCategoryModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var jsonResult = Json(repository.Add(addCategoryModel));
        jsonResult.StatusCode = StatusCodes.Status201Created;
        
        return jsonResult;
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetCategoryById([FromRoute] Guid id)
    {
        var categoryModel = repository.GetById(id);

        if (categoryModel == null)
            return NotFound();
        
        return Json(categoryModel);
    }
        
    [HttpGet("search")]
    // [Authorize(Roles = AuthUserModel.ADMIN_ROLE)]
    public IActionResult SearchCategoryByName([FromQuery] string name)
    {
        var categoryModel = repository.GetByName(name);
        
        if (categoryModel == null)
            return NotFound();
        
        return Json(categoryModel);
    }

    [HttpPut]
    // [Authorize(Roles = AuthUserModel.ADMIN_ROLE)]
    public IActionResult PutCategory([FromBody] PutCategoryModelDto categoryModelDto)
    {
        var categoryModel = repository.Put(categoryModelDto);
        
        if (categoryModel == null)
            return NotFound();
        
        return Json(categoryModel);
    }
    
    [HttpPatch]
    // [Authorize(Roles = AuthUserModel.ADMIN_ROLE)]
    public IActionResult PatchCategory([FromBody] PatchCategoryModelDto categoryModelDto)
    {
        var categoryModel = repository.Patch(categoryModelDto);
        
        if (categoryModel == null)
            return NotFound();
        
        return Json(categoryModel);
    }

    [HttpDelete("{id:guid}")]
    // [Authorize(Roles = AuthUserModel.ADMIN_ROLE)]
    public IActionResult DeleteCategory(Guid id)
    {
        var categoryModel = repository.DeleteById(id);
        
        if (categoryModel == null)
            return NotFound();
        
        return Json(categoryModel);
    }
    
    [HttpGet]
    public IActionResult GetCatalog()
    {
        var catalogModels = repository.GetAll();
        var jsonResult = Json(catalogModels);

        if (!catalogModels.Any())
            jsonResult.StatusCode = StatusCodes.Status204NoContent;

        return jsonResult;
    }
}
