using Microsoft.AspNetCore.Mvc;
using AgentContract;
using Orleans;

namespace AgentClient.Controllers;

[ApiController]
[Route("api/Agent")]
[Produces("application/json")]
public class AgentAgentController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private IClusterClient orleansClient;

    private readonly ILogger<AgentAgentController> _logger;

    public AgentAgentController(ILogger<AgentAgentController> logger, IClusterClient orleansClient)
    {
        this.orleansClient = orleansClient;
        _logger = logger;
    }

    [HttpGet("{agentId}")]
    public Task<AgentData> GetAgentData(string agentStringId)
    {
        Guid agentId = Guid.Parse(agentStringId); //TODO try block or other check on input
        var grain = this.orleansClient.GetGrain<IAgentGrain>(agentId);
        return grain.GetAgentData();
    }
}
