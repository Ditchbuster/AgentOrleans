using AgentGrainInterfaces;
using Microsoft.Extensions.Logging;

namespace AgentGrains;

public class Zone : Grain, IZone
{
    private readonly ILogger _logger;

    public Zone(ILogger<Zone> logger) => _logger = logger;

    private readonly int _location;

     public override Task OnActivateAsync(CancellationToken cancellationToken)
    {
        
        return base.OnActivateAsync(cancellationToken);
    }
    ValueTask<string> IZone.SayHello(string greeting)
    {
        _logger.LogInformation("""
            SayHello message received: greeting = "{Greeting}"
            """,
            greeting);
        
        return ValueTask.FromResult($"""

            Client said: "{greeting}", so HelloGrain ({_location})says: Hello!
            """);
    }

    public Task<Guid[]> GetTasks() //TODO eventually return tasks(task data objects) availible here, accept filtering params
    {
        return Task.FromResult(new[]{Guid.NewGuid()});
    }
}