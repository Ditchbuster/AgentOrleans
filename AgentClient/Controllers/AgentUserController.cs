using Microsoft.AspNetCore.Mvc;
using AgentContract;
namespace AgentClient.Controllers;

[ApiController]
[Route("api/User")]
[Produces("application/json")]
public class AgentUserController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<AgentUserController> _logger;

    public AgentUserController(ILogger<AgentUserController> logger)
    {
        _logger = logger;
    }

    [HttpGet("{userId}")]
    public IEnumerable<UserData> GetUserData(string userId)
    {
        return Enumerable.Range(1, 5).Select(index => new UserData
        {
            UserId = Guid.NewGuid(),
            name = Summaries[Random.Shared.Next(Summaries.Length)],
        })
        .ToArray();
    }
}
