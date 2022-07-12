using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Orleans;
using Contracts;

namespace Client.Controllers
{
    [ApiController]
    [Produces("application/json")]
    /* [Route("api/Games")]
    public class GamesController : Controller
    {
        private IClusterClient orleansClient;

        public GamesController(IClusterClient orleansClient)
        {
            this.orleansClient = orleansClient;
        }

        [HttpGet]
        public async Task<List<string>> Get(int gameId)
        {
            var grain = this.orleansClient.GetGrain<IGameGrain>(gameId);
            return grain.ListPlayersAsync();
        }

        [HttpPut]
        public async Task Put(int gameId, string playerName)
        {
            var grain = this.orleansClient.GetGrain<IGameGrain>(gameId);
            await grain.JoinAsync(playerName);
        }

        [HttpDelete]
        public async Task Delete(int gameId, string playerName)
        {
            var grain = this.orleansClient.GetGrain<IGameGrain>(gameId);
            await grain.LeaveAsync(playerName);
        }


    } */

    [Route("api/Agent")]
    public class AgentController : Controller
    {
        private IClusterClient orleansClient;

        public AgentController(IClusterClient orleansClient)
        {
            this.orleansClient = orleansClient;
        }

        [HttpGet]
        public async Task<AgentData> Get(string agentId)
        {
            var grain = this.orleansClient.GetGrain<IAgentGrain>(agentId);
            AgentData agent = new AgentData();
            agent.agentId = Guid.Parse(grain.GetPrimaryKeyString());
            agent.name = await grain.GetAgentName();
            //return Task.FromResult(agent);
            return agent;
            
        }
        [HttpGet("{agentId}")]
        public Task<string> GetId(string agentId)
        {
            var grain = this.orleansClient.GetGrain<IAgentGrain>(agentId);
            return Task.FromResult(grain.GetAgentName().Result + " " + grain.GetAgentSkill().Result.ToString());
        }
        [HttpGet("toggle")]
        public Task ToggleActivity(string agentId)
        {
            var grain = this.orleansClient.GetGrain<ITaskGrain>(Guid.Parse(agentId));
            grain.StartTask();
            return Task.CompletedTask;
        }
    }

    [Route("api/Users")]
    //[Authorize]
    public class UserController : Controller
    {
        private IClusterClient orleansClient;

        public UserController(IClusterClient orleansClient)
        {
            this.orleansClient = orleansClient;
        }

        [HttpGet]
        public Task<UserData> Get(string userId)
        {

            var grain = this.orleansClient.GetGrain<IUserGrain>(Guid.Parse(userId));
            /* string claimString = "";
            foreach (var c in User.Claims)
            {
                claimString += c.Type + ":" + c.Value + "\n";
            } */
            UserData userData = new userData();
            userData.userId = userId;
            return Task.FromResult<UserData>(userData);
        }
        [HttpPost]
        public Task<Guid> Post()
        {
            //create new user
            Guid newId = Guid.NewGuid();
            return Task.FromResult(newId);
        }
        [HttpPut("{agentId}")]
        public Task Put(string agentId)
        {
            return Task.CompletedTask;
        }
    }

    [Route("api/Locations")]
    //[Authorize]
    public class LocationController : Controller
    {
        private IClusterClient orleansClient;

        public LocationController(IClusterClient orleansClient)
        {
            this.orleansClient = orleansClient;
        }

        [HttpGet]
        public Task<LocationData> Get(string LocationId)
        {

            var grain = this.orleansClient.GetGrain<ILocationGrain>(Guid.Parse(LocationId));
            return grain.GetData();
        }
        [HttpPost]
        public Task Post(string LocationId)
        {
            var grain = this.orleansClient.GetGrain<ILocationGrain>(Guid.Parse(LocationId));
            return grain.SetControlLevel(5);
        }
        [HttpPut("{LocationId}")]
        public Task Put(string LocationId)
        {
            var grain = this.orleansClient.GetGrain<ILocationGrain>(Guid.Parse(LocationId));
            return grain.SetControlLevel(5);
        }
    }
}