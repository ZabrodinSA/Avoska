using Avoska.Models.Catalogs;

namespace Avoska.Repositories.Catalogs;

public interface ICategoriesRepository
{
    public IEnumerable<CategoryModel> GetAll();
    
    public CategoryModel? GetById(Guid id);

    public CategoryModel? GetByName(string name);
    
    public CategoryModel Add(AddCategoryModelDto addCategoryModelDto);

    public CategoryModel? Put(PutCategoryModelDto goodInfo);
    
    public CategoryModel? Patch(PatchCategoryModelDto goodInfo);
    
    public CategoryModel? DeleteById(Guid id);
}