using FusionQA.Feb2020.Client;
using FusionQA.Feb2020.Server.Blazor;
using FusionQA.Feb2020.Server.Services;
using FusionQA.Feb2020.Shared;
using Stl.Fusion;
using Stl.Fusion.Extensions;
using Stl.Fusion.Server;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var cfg = builder.Configuration;

// Options
services.Configure<BlazorHybridOptions>(cfg.GetSection(nameof(BlazorHybridOptions)));

// Fusion
var fusion = services.AddFusion();
var fusionServer = fusion.AddWebServer();

SharedServices.Configure(services);

// Fusion services
fusion.AddFusionTime(); // IFusionTime is one of built-in compute services you can use
fusion.AddComputeService<ICounterService, CounterService>();
fusion.AddComputeService<IWeatherForecastService, WeatherForecastService>();

services.AddControllersWithViews();
services.AddRazorPages();
services.AddServerSideBlazor();

services.AddEndpointsApiExplorer();
services.AddSwaggerDocument();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseWebAssemblyDebugging();
    app.UseOpenApi();
    app.UseSwaggerUi3();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    //app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseWebSockets(new WebSocketOptions() {
    KeepAliveInterval = TimeSpan.FromSeconds(30),
});

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.Use((context, next) => {
    var hybridIdCookieName = "hybrid-instance-id";
    if (context.Request.Cookies.All(c => c.Key != hybridIdCookieName)) {
        var idCookieOptions = new CookieOptions {
            Path = "/",
            Secure = true,
            HttpOnly = true,
            IsEssential = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.Now.AddYears(100),
        };
        context.Response.Cookies.Append(
            key: hybridIdCookieName,
            value: Guid.NewGuid().ToString(),
            options: idCookieOptions);
        return next();
    }
    return next();
});

app.MapFusionWebSocketServer();
app.MapRazorPages();
app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
