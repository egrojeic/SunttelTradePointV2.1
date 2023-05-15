using IdentityServer4.Services;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using SunttelTradePointB.Identity;

var builder = WebApplication.CreateBuilder(args);

// It was needed to install the following NuGet packages:
// - IdentityServer4
// To tested:
// https://localhost:7125/.well-known/openid-configuration


builder.Services.AddControllersWithViews();

builder.Services.AddIdentityServer()
    .AddInMemoryClients(Config.Clients)
    .AddInMemoryIdentityResources(Config.IdentityResources)
    //.AddInMemoryApiResources(Config.ApiResources)
    .AddInMemoryApiScopes(Config.ApiScopes)
    .AddTestUsers(Config.TestUsers)
    .AddDeveloperSigningCredential();


builder.Services.AddSingleton<ICorsPolicyService>((container) => {
var logger = container.GetRequiredService<ILogger<DefaultCorsPolicyService>>();
return new DefaultCorsPolicyService(logger)
{
AllowAll = true
};
});

builder.Services.Configure<MvcOptions>(options =>
{
options.Filters.Add(new ContentSecurityPolicyMiddleware());
});

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseIdentityServer();

app.MapDefaultControllerRoute();
//app.MapGet("/", () => "Hello World!");
app.UseCors("AllowAll");
//app.UseCors();

// Add the following lines to log the request headers and origin:
app.Use(async (context, next) =>
{
var origin = context.Request.Headers["Origin"].ToString();
Console.WriteLine($">>> Request from origin: {origin}");
await next.Invoke();
});

app.Use(async (context, next) =>
{
context.Response.Headers.Add("Content-Security-Policy", "default-src 'self'; connect-src 'self' wss://localhost:44364");
await next();
});

app.UseAuthorization();


app.Run();



public class ContentSecurityPolicyMiddleware : IActionFilter
{
    public void OnActionExecuted(ActionExecutedContext context)
    {
        // Remove any existing Content-Security-Policy headers
        context.HttpContext.Response.Headers.Remove("Content-Security-Policy");


        context.HttpContext.Response.Headers.Add("Content-Security-Policy", "frame-ancestors 'self' https://localhost:7213");
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        // Do nothing
    }
}
