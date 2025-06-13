using Avoska.Models.Catalogs;
using Avoska.Models.Goods;
using Avoska.Models.Users;
using Avoska.Repositories.Catalogs;
using Avoska.Repositories.Goods;
using Avoska.Repositories.Users;
using Avoska.Services;
using Avoska.Services.Validators.Users;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Support services
builder.Services.AddTransient<IPhoneAuthService, PhoneAuthService>();
builder.Services.AddLogging();

//Repository services
builder.Services.AddTransient<IUsersInfoRepository, ManagementUsersInfoRepository>();
builder.Services.AddTransient<IGoodsInfoRepository, DbGoodsInfoRepository>();
builder.Services.AddTransient<ICategoriesRepository, DbCategoriesRepository>();

//Validator services
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddScoped<IValidator<VerifyCodeModelDto>, VerifyCodeModelDtoValidator>();
builder.Services.AddScoped<IValidator<PutUserInfoModelDto>, PutUserInfoModelDtoValidator>();

//Add DB contexts
builder.Services.AddDbContext<UserInfoContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("UsersInfoConnection")));
builder.Services.AddDbContext<GoodsInfoContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("GoodsInfoConnection")));
builder.Services.AddDbContext<CategoriesContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("CategoriesConnection")));

//Setting identity
builder.Services.AddIdentity<UserInfoModel, IdentityRole>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.SignIn.RequireConfirmedEmail = false; 
        options.Password.RequiredLength = 0; 
    })
    .AddEntityFrameworkStores<UserInfoContext>()
    .AddDefaultTokenProviders();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//TODO Удобно чистить базу пока так
// using (var scope = app.Services.CreateScope())
// {
//     var dbContext = scope.ServiceProvider.GetRequiredService<UserInfoContext>();
//     await dbContext.Database.EnsureDeletedAsync(); // Удаляем существующую БД
//     await dbContext.Database.EnsureCreatedAsync(); // Создаем новую с актуальной схемой
//     
//     var dbContextGoods = scope.ServiceProvider.GetRequiredService<GoodsInfoContext>();
//     await dbContextGoods.Database.EnsureDeletedAsync(); // Удаляем существующую БД
//     await dbContextGoods.Database.EnsureCreatedAsync(); // Создаем новую с актуальной схемой
//     
//     var dbContextCategory = scope.ServiceProvider.GetRequiredService<CategoriesContext>();
//     await dbContextCategory.Database.EnsureDeletedAsync(); // Удаляем существующую БД
//     await dbContextCategory.Database.EnsureCreatedAsync(); // Создаем новую с актуальной схемой
// }

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => 
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Avoska API V1");
    });
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication(); 
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllers();

app.Run();