using AgentGrainInterfaces;
using Microsoft.Extensions.Logging;
using Orleans.Runtime;

namespace AgentGrains;

public class Agent : Grain, IAgent
{
    private readonly ILogger _logger;
    private readonly IPersistentState<AgentState> _state;
    public Agent(ILogger<Agent> logger,[PersistentState("agent", "Store")]
        IPersistentState<AgentState> state){ 
        _logger = logger;
        _state = state;
    }
    private readonly int _location;

     public override Task OnActivateAsync(CancellationToken cancellationToken)
    {
        
        return base.OnActivateAsync(cancellationToken);
    }
    ValueTask<string> IAgent.SayHello(string greeting)
    {
        string msg = string.Format($"""
            Client said: "{greeting}", so Agent({this.GetPrimaryKeyString()}) says: Hello! XP:{this._state.State.xp}
            """);
        _logger.LogInformation("""
            {0}.SayHello: greeting="{Greeting}", xp={xp}
            """,
            this.GetPrimaryKeyString(),greeting,_state.State.xp);
        
        return ValueTask.FromResult(msg);
    }
    
    async Task IAgent.AddExperence(int xpAmount){
        _state.State.xp = _state.State.xp+xpAmount;
        await _state.WriteStateAsync();
    }

}