namespace AgentGrainInterfaces;

public interface IZone : IGrainWithStringKey //this is a string key to make up my own unique way of identifying ie X.Y.Z
{
    ValueTask<string> SayHello(string greeting);

    Task<Guid[]> GetTasks();
}