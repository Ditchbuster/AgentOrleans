[Serializable]
public class AgentState
{
    public required string name { get; set; }

    public int xp = 0;
    public Guid taskId = Guid.Empty;
}