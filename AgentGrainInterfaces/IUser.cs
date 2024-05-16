namespace AgentGrainInterfaces;

public interface IUser : IGrainWithIntegerKey
{
    ValueTask<string> SayHello(string greeting);
}