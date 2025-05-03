using Avoska.Models.Catalogs;
using Microsoft.EntityFrameworkCore;

namespace Avoska.Repositories.Catalogs;

public class DbCategoriesRepository (
    CategoriesContext db,
    ILogger<DbCategoriesRepository> logger)
    : ICategoriesRepository
{
    public Task<IEnumerable<CategoryModel>> GetAll()
    {
        return Task.FromResult<IEnumerable<CategoryModel>>(db.Categories);
    }

    public async Task<CategoryModel?> GetById(Guid id)
    {
        return await db.Categories.FirstOrDefaultAsync(category => category.Id == id);
    }

    public async Task<CategoryModel?> GetByName(string name)
    {
        return await db.Categories.FirstOrDefaultAsync(category => category.Name == name);
    }

    public async Task<CategoryModel?> Add(AddCategoryModelDto addCategoryModelDto)
    {
        var categoryModel = await db.Categories.FirstOrDefaultAsync(good => good.Name == addCategoryModelDto.Name);
        if (categoryModel != null)
        {
            logger.Log(LogLevel.Error, "Попытка добавить куатегорию с занятым именем");
            return null;
        }
        
        categoryModel = new CategoryModel();
        categoryModel.Update(addCategoryModelDto);

        db.Categories.Add(categoryModel);
        return await UpdateDb() ? categoryModel : null;
    }

    public async Task<CategoryModel?> Put(PutCategoryModelDto putCategoryDto)
    {
        var categoryModel = await db.Categories.FirstOrDefaultAsync(category => category.Id == putCategoryDto.Id);
        if (categoryModel == null) return null;
        
        categoryModel.Update(putCategoryDto);
        
        return await SaveCategory(categoryModel) ? categoryModel : null;
    }

    public async Task<CategoryModel?> Patch(PatchCategoryModelDto patchCategoryDto)
    {
        var categoryModel = await db.Categories.FirstOrDefaultAsync(category => category.Id == patchCategoryDto.Id);
        if (categoryModel == null) return null;
        
        categoryModel.Update(patchCategoryDto);
        
        return await SaveCategory(categoryModel) ? categoryModel : null;
    }

    public async Task<CategoryModel?> DeleteById(Guid id)
    {
        var categoryModel = await db.Categories.FirstOrDefaultAsync(category => category.Id == id);
        if (categoryModel == null) return null;
        
        db.Categories.Remove(categoryModel);
        
        return await UpdateDb() ? categoryModel : null;
    }
    
    private async Task<bool> SaveCategory(CategoryModel category)
    {
        try
        {
            db.Categories.Update(category);
            await db.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException ex)
        {
            logger.Log(LogLevel.Error, ex.ToString());
            return false;
        }
    }
    
    private async Task<bool> UpdateDb()
    {
        try
        {
            await db.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException ex)
        {
            logger.Log(LogLevel.Error, ex.ToString());
            return false;
        }
    }
}