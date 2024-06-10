namespace AgentGrainInterfaces;

public interface ITask : IGrainWithGuidKey
{
    ValueTask<string> SayHello(string greeting);
    Task<bool> StartTask();
    Task SetTaskInfo(string agentId, int minutes);
    
}