namespace AgentGrainInterfaces;

public interface IZone : IGrainWithStringKey
{
    ValueTask<string> SayHello(string greeting);
}