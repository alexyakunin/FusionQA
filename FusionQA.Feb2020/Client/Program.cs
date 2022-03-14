using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using FusionQA.Feb2020.Client;
using FusionQA.Feb2020.Client.Services;
using FusionQA.Feb2020.Shared;
using Stl.Fusion;
using Stl.Fusion.Blazor;
using Stl.Fusion.Client;
using Stl.Fusion.Extensions;
using Stl.Fusion.UI;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
var services = builder.Services;
var baseUri = new Uri(builder.HostEnvironment.BaseAddress);
var apiBaseUri = new Uri($"{baseUri}api");

// Fusion
var fusion = services.AddFusion();
var fusionClient = fusion.AddRestEaseClient(
    (c, o) => {
        o.BaseUri = baseUri;
        o.IsLoggingEnabled = true;
        o.IsMessageLoggingEnabled = false;
    }).ConfigureHttpClientFactory(
    (c, name, o) => {
        var isFusionClient = (name ?? "").StartsWith("Stl.Fusion");
        var clientBaseUri = isFusionClient ? baseUri : apiBaseUri;
        o.HttpClientActions.Add(client => client.BaseAddress = clientBaseUri);
    });

// Fusion services
fusion.AddFusionTime();

// Fusion service clients
fusionClient.AddReplicaService<ICounterService, ICounterClientDef>();
fusionClient.AddReplicaService<IWeatherForecastService, IWeatherForecastClientDef>();

// Fusion Blazor UI services
fusion.AddBlazorUIServices();
// Default update delayer w/ 0.5s update delay 
services.AddTransient<IUpdateDelayer>(c => new UpdateDelayer(c.GetRequiredService<IUICommandTracker>(), 0.5)); 

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

await builder.Build().RunAsync();
