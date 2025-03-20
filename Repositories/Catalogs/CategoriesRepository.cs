using Avoska.Models.Catalogs;

namespace Avoska.Repositories.Catalogs;

public class CategoriesRepository : ICategoriesRepository
{
    private static readonly Dictionary<Guid, CategoryModel> CategoryModelMap = [];

    public IEnumerable<CategoryModel> GetAll()
    {
        return CategoryModelMap.Values;
    }

    public CategoryModel? GetById(Guid id)
    {
        return CategoryModelMap.GetValueOrDefault(id);
    }

    public CategoryModel? GetByName(string? name)
    {
        return CategoryModelMap.Values.FirstOrDefault(o => o.Name == name);
    }

    public CategoryModel Add(AddCategoryModelDto addCategoryModelDto)
    {
        var categoryModel = new CategoryModel(addCategoryModelDto);

        CategoryModelMap.Add(categoryModel.Id, categoryModel);

        return categoryModel;    
    }

    public CategoryModel? Put(PutCategoryModelDto putCategory)
    {
        if (!CategoryModelMap.TryGetValue(putCategory.Id, out var categoryModel))
            return null;

        categoryModel.Update(putCategory);
        
        return categoryModel;
    }

    public CategoryModel? Patch(PatchCategoryModelDto patchCategory)
    {
        if (!CategoryModelMap.TryGetValue(patchCategory.Id, out var categoryModel))
            return null;

        categoryModel.Update(patchCategory);
        
        return categoryModel;    }

    public CategoryModel? DeleteById(Guid id)
    {
        if (!CategoryModelMap.Remove(id, out var categoryModel))
            return null;

        return categoryModel;
    }
}