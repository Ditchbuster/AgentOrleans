[GenerateSerializer]
[Alias("UserData")]
public record UserData( string name,
    Guid primaryAgentId){
    
    [Id(0)]
    public string name { get; init; } = name ?? "Test";

    [Id(1)]
    public Guid primaryAgentId { get; init; } = primaryAgentId;
   
}