using Avoska;
using Avoska.Models.Users;
using Avoska.Repositories.Catalogs;
using Avoska.Repositories.Goods;
using Avoska.Repositories.Users;
using Avoska.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Repository services
builder.Services.AddTransient<IUsersInfoRepository, UsersInfoRepository>();
builder.Services.AddTransient<IGoodsInfoRepository, GoodsInfoRepository>();
builder.Services.AddTransient<ICategoriesRepository, CategoriesRepository>();

//Support services
builder.Services.AddTransient<IPhoneAuthService, PhoneAuthService>();

//Add DB contexts
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AuthUserDbContext>(options => options.UseSqlServer(connection));

//Setting identity
builder.Services.AddIdentity<AuthUserModel, IdentityRole>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.SignIn.RequireConfirmedEmail = false; 
        options.Password.RequiredLength = 0; 
    })
    .AddEntityFrameworkStores<AuthUserDbContext>()
    .AddDefaultTokenProviders();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication(); 
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllers();

app.Run();