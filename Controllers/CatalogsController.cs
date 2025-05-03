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
    public async Task<IActionResult> AddCategory([FromBody] AddCategoryModelDto addCategoryModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var categoryModel = await repository.GetByName(addCategoryModel.Name);
        if (categoryModel != null)
        {
            return BadRequest($"Имя категории [{addCategoryModel.Name}] уже занято");
        }
        
        var jsonResult = Json(await repository.Add(addCategoryModel));
        jsonResult.StatusCode = StatusCodes.Status201Created;
        
        return jsonResult;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetCategoryById([FromRoute] Guid id)
    {
        var categoryModel = await repository.GetById(id);
        
        return categoryModel != null ? Json(categoryModel) : NotFound();
    }
        
    [HttpGet("search")]
    // [Authorize(Roles = AuthUserModel.ADMIN_ROLE)]
    public async Task<IActionResult> SearchCategory([FromQuery] string name)
    {
        var categoryModel = await repository.GetByName(name);
        
        return categoryModel != null ? Json(categoryModel) : NotFound();
    }

    [HttpPut]
    // [Authorize(Roles = AuthUserModel.ADMIN_ROLE)]
    public async Task<IActionResult> PutCategory([FromBody] PutCategoryModelDto categoryModelDto)
    {
        var categoryModel = await repository.Put(categoryModelDto);
        
        return categoryModel != null ? Json(categoryModel) : NotFound();
    }
    
    [HttpPatch]
    // [Authorize(Roles = AuthUserModel.ADMIN_ROLE)]
    public async Task<IActionResult> PatchCategory([FromBody] PatchCategoryModelDto categoryModelDto)
    {
        var categoryModel = await repository.Patch(categoryModelDto);
        
        return categoryModel != null ? Json(categoryModel) : NotFound();
    }

    [HttpDelete("{id:guid}")]
    // [Authorize(Roles = AuthUserModel.ADMIN_ROLE)]
    public async Task<IActionResult> DeleteCategory(Guid id)
    {
        var categoryModel = await repository.DeleteById(id);
        
        return categoryModel != null ? Json(categoryModel) : NotFound();
    }
    
    [HttpGet]
    public async Task<IActionResult> GetCatalog()
    {
        var catalogModels = await repository.GetAll();
        var jsonResult = Json(catalogModels);

        if (!catalogModels.Any())
            jsonResult.StatusCode = StatusCodes.Status204NoContent;

        return jsonResult;
    }
}
