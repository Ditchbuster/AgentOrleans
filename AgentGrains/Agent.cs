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
    public Task<AgentData> GetInfo()
    {
        return Task.FromResult(new AgentData(this._state.State.name, this._state.State.xp));
    }

    public async Task<bool> SetBusy(bool busy, Guid taskId)
    {
        if (busy == true){
        if (this._state.State.taskId == Guid.Empty){
            this._state.State.taskId = taskId;
            await this._state.WriteStateAsync();
            return true;
        }else{
            _logger.LogError("Agent:{0} is already busy with {1}",
            this.GetPrimaryKeyString(),this._state.State.taskId);
            return false; //TODO proper error handling?
        }}else
        {
            this._state.State.taskId = Guid.Empty;
            await this._state.WriteStateAsync();
            return true;   
        }
    }
}