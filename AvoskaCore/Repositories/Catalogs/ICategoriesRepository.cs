using Avoska.Models.Catalogs;

namespace Avoska.Repositories.Catalogs;

public interface ICategoriesRepository
{
    public Task<IEnumerable<CategoryModel>> GetAll();
    
    public Task<CategoryModel?> GetById(Guid id);

    public Task<CategoryModel?> GetByName(string name);
    
    public Task<CategoryModel?> Add(AddCategoryModelDto addCategoryModelDto);

    public Task<CategoryModel?> Put(PutCategoryModelDto putCategoryDto);
    
    public Task<CategoryModel?> Patch(PatchCategoryModelDto patchCategoryDto);
    
    public Task<CategoryModel?> DeleteById(Guid id);
}