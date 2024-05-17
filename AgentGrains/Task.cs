using AgentGrainInterfaces;
using Microsoft.Extensions.Logging;
using Orleans.Runtime;

namespace AgentGrains;

public class AgentTask : Grain, ITask, IRemindable
{
    private readonly ILogger _logger;
private int _length;
private Guid _agentId;

    private IGrainReminder? _reminder;
    public AgentTask(ILogger<AgentTask> logger) => _logger = logger;

    private readonly int _location;

     public override Task OnActivateAsync(CancellationToken cancellationToken)
    {
        
        return base.OnActivateAsync(cancellationToken);
    }
    ValueTask<string> ITask.SayHello(string greeting)
    {
        _logger.LogInformation("""
            SayHello message received: greeting = "{Greeting}"
            """,
            greeting);
        
        return ValueTask.FromResult($"""

            Client said: "{greeting}", so HelloGrain ({_location})says: Hello!
            """);
    }

    public async Task<bool> StartTask(string agentId){
        _reminder = await this.RegisterOrUpdateReminder(
            reminderName: this.GetPrimaryKeyString(),
            dueTime: TimeSpan.Zero,
            period: TimeSpan.FromMinutes(_length));
        return true;
    }

    public async Task ReceiveReminder(string reminderName, TickStatus status)
    {
        IAgent agent = GrainFactory.GetGrain<IAgent>(_agentId);
        agent.AddExperence(10);
        Console.WriteLine("Task finished {0}",_agentId);
        agent.SayHello("");
        await this.UnregisterReminder(await this.GetReminder(reminderName)); //todo catch null?
        return;
        //throw new NotImplementedException();
    }
    public Task SetTaskInfo(string agentId, int minutes){
        //todo check if task is running first
        _agentId = new Guid(agentId);
        _length = minutes;
        return Task.CompletedTask;
    }
}