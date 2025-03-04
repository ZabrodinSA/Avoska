using Avoska;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<ITimeService, SimpleTimeService>(); // добавляем сервис ITimeService

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

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllers();

app.Run();


public interface ITimeService
{
    string Time { get; }
}
public class SimpleTimeService: ITimeService
{
    public string Time => DateTime.Now.ToString("hh:mm:ss");
}