using AgentGrainInterfaces;
using Microsoft.Extensions.Logging;
using Orleans.Runtime;

namespace AgentGrains;

public class User : Grain, IUser
{
    private readonly ILogger _logger;
    private readonly IPersistentState<UserState> _state;
    public User(ILogger<User> logger,[PersistentState("user", "Store")]
        IPersistentState<UserState> state){ 
        _logger = logger;
        _state = state;
    }
    private readonly int _location;

     public override Task OnActivateAsync(CancellationToken cancellationToken)
    {
        this._state.State.name = this.GetPrimaryKeyString();  
        return base.OnActivateAsync(cancellationToken);
    }
    ValueTask<string> IUser.SayHello(string greeting)
    {
        string msg = string.Format($"""
            Client said: "{greeting}", so User({this.GetPrimaryKeyString()}) says: AgentId:{this._state.State.primaryAgentId}!
            """);
        _logger.LogInformation("""
            {0}.SayHello: greeting="{Greeting}"
            """,
            this.GetPrimaryKeyString(),greeting);
        
        return ValueTask.FromResult(msg);
    }

    public Task<UserData> GetInfo()
    {
        return Task.FromResult(new UserData(this._state.State.name, this._state.State.primaryAgentId));
    }
   

}