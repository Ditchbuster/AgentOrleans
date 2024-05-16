using AgentGrainInterfaces;
using Microsoft.Extensions.Logging;
using Orleans.Runtime;

namespace AgentGrains;

public class AgentTask : Grain, ITask, IRemindable
{
    private readonly ILogger _logger;
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
            period: TimeSpan.FromMinutes(2));
        return true;
    }

    public Task ReceiveReminder(string reminderName, TickStatus status)
    {
        Console.WriteLine("Thanks for reminding me-- I almost forgot!");
    return Task.CompletedTask;
        //throw new NotImplementedException();
    }
}