using FusionQA.Feb2020.Shared;
using Microsoft.AspNetCore.Mvc;
using Stl.Fusion.Server;

namespace FusionQA.Feb2020.Server.Controllers;

[Route("api/[controller]/[action]")]
[ApiController, JsonifyErrors]
public class CounterController : ControllerBase, ICounterService
{
    private readonly ICounterService _counter;

    public CounterController(ICounterService counter) => _counter = counter;

    [HttpGet, Publish]
    public Task<int> Get(CancellationToken cancellationToken = default)
        => _counter.Get(cancellationToken);

    [HttpPost]
    public Task Increment(CancellationToken cancellationToken = default)
        => _counter.Increment(cancellationToken);
}
