namespace FusionQA.Mar2022.Server.Blazor;

public static class BlazorHybridTypeExt
{
    public static bool IsHybrid(this BlazorHybridType blazorHybridType)
        => blazorHybridType switch {
            BlazorHybridType.HybridManual => true,
            BlazorHybridType.HybridOnNavigation => true,
            BlazorHybridType.HybridOnReady => true,
            _ => false,
        };
}
