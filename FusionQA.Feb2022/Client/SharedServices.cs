﻿using Stl.Fusion;
using Stl.Fusion.Blazor;
using Stl.Fusion.Extensions;
using Stl.Fusion.UI;

namespace FusionQA.Feb2022.Client;

public static class SharedServices
{
    public static void Configure(IServiceCollection services)
    {
        var fusion = services.AddFusion();
        
        fusion.AddBlazorUIServices();
        fusion.AddFusionTime();
        
        services.AddTransient<IUpdateDelayer>(c => new UpdateDelayer(c.GetRequiredService<IUICommandTracker>(), 0.1)); 
    }
}
