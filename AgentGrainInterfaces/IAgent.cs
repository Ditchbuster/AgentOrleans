namespace AgentGrainInterfaces;

public interface IAgent : IGrainWithGuidKey
{
    ValueTask<string> SayHello(string greeting);
    Task AddExperence(int xpAmount);
    
    Task<AgentData> GetInfo();
}