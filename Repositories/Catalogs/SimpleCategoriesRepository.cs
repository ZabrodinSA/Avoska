using Avoska.Models.Catalogs;

namespace Avoska.Repositories.Catalogs;

public class SimpleCategoriesRepository : ICategoriesRepository
{
    private static readonly Dictionary<Guid, CategoryModel> CategoryModelMap = [];

    public Task<IEnumerable<CategoryModel>> GetAll()
    {
        return Task.FromResult<IEnumerable<CategoryModel>>(CategoryModelMap.Values);
    }

    public Task<CategoryModel?> GetById(Guid id)
    {
        return Task.FromResult(CategoryModelMap.GetValueOrDefault(id));
    }

    public Task<CategoryModel?> GetByName(string? name)
    {
        return Task.FromResult(CategoryModelMap.Values.FirstOrDefault(o => o.Name == name));
    }

    public Task<CategoryModel?> Add(AddCategoryModelDto addCategoryModelDto)
    {
        var categoryModel = new CategoryModel();
        categoryModel.Update(addCategoryModelDto);

        CategoryModelMap.Add(categoryModel.Id, categoryModel);

        return Task.FromResult(categoryModel)!;    
    }

    public Task<CategoryModel?> Put(PutCategoryModelDto putCategoryDto)
    {
        CategoryModelMap.TryGetValue(putCategoryDto.Id, out var categoryModel);

        categoryModel?.Update(putCategoryDto);
        
        return Task.FromResult(categoryModel);
    }

    public Task<CategoryModel?> Patch(PatchCategoryModelDto patchCategoryDto)
    {
        CategoryModelMap.TryGetValue(patchCategoryDto.Id, out var categoryModel);

        categoryModel?.Update(patchCategoryDto);
        
        return Task.FromResult(categoryModel);    }

    public Task<CategoryModel?> DeleteById(Guid id)
    {
        if (!CategoryModelMap.Remove(id, out var categoryModel));

        return Task.FromResult(categoryModel);
    }
}