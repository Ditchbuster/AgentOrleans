namespace AgentGrainInterfaces;

public interface ITask : IGrainWithGuidKey
{
    ValueTask<string> SayHello(string greeting);
    Task<bool> StartTask(string agentId);
    Task SetTaskInfo(string agentId, int minutes);
    
}