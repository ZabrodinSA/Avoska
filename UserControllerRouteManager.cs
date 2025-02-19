namespace Avoska;

public static class UserControllerRouteManager
{
    public static void AddControllerRoute(WebApplication app)
    {
        app.MapControllerRoute(
            name: "SendUserPhone",
            pattern: "users",
            defaults: new { controller = "Users", action = "SendUserPhone" }
        );

        app.MapControllerRoute(
            name: "SendAuthCode",
            pattern: "users/{id:int}",
            defaults: new { controller = "Users", action = "SendAuthCode" }
        );
    }
}