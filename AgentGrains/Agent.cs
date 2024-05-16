using AgentGrainInterfaces;
using Microsoft.Extensions.Logging;

namespace AgentGrains;

public class Agent : Grain, IAgent
{
    private readonly ILogger _logger;
    private int _xp; //TODO move to state, update for different types of xp
    public Agent(ILogger<Agent> logger) => _logger = logger;

    private readonly int _location;

     public override Task OnActivateAsync(CancellationToken cancellationToken)
    {
        
        return base.OnActivateAsync(cancellationToken);
    }
    ValueTask<string> IAgent.SayHello(string greeting)
    {
        _logger.LogInformation("""
            SayHello message received: greeting = "{Greeting}"
            """,
            greeting);
        
        return ValueTask.FromResult($"""

            Client said: "{greeting}", so Agent({this.GetPrimaryKeyString()}) says: Hello! XP:{this._xp}
            """);
    }
    
    Task IAgent.AddExperence(int xpAmount){
        _xp = _xp+xpAmount;
        return Task.CompletedTask;
    }

}