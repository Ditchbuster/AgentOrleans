[GenerateSerializer]
[Alias("AgentData")]
public record AgentData( string name, int xp){
    
    [Id(0)]
    public string name { get; init; } = name ?? "Test";

    [Id(2)]
    public int xp {get; init;} = xp;
}