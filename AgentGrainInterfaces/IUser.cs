namespace AgentGrainInterfaces;

public interface IUser : IGrainWithStringKey //TODO change to GUID or other id and index outside of orleans
{
    ValueTask<string> SayHello(string greeting);
    ValueTask<string> GetAgentInfo(string agentId);
}