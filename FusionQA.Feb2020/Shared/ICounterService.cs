using Stl.Fusion;

namespace FusionQA.Feb2020.Shared;

public interface ICounterService
{
    [ComputeMethod]
    Task<int> Get(CancellationToken cancellationToken = default);
    Task Increment(CancellationToken cancellationToken = default);
}
