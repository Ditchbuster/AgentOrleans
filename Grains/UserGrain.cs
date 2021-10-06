using Contracts;
using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grains
{
    public class UserGrain : Grain, IUserGrain
    {
        private string name;
        private string state;
        private string agentId;
        private List<string> userCommands;

        public UserGrain()
        {
            this.name = "test";
            this.state = "new";
            this.agentId = "a1";
            this.userCommands = new List<string>();
        }

        public Task<string> GetUserName()
        {
            return Task.FromResult(this.name);
        }

        public Task<string> GetUserState()
        {
            return Task.FromResult(this.state);
        }

        public Task QueueCommand(string msg)
        {
            //TODO any logic to clean or reject the command.. 
            userCommands.Append(msg);
            return Task.CompletedTask;
        }

        public Task StartMission(int taskId, string agentId)
        {
            GrainFactory.GetGrain<ITaskGrain>(taskId).AddTaskEntity(agentId);
            GrainFactory.GetGrain<ITaskGrain>(taskId).StartTask();
            return Task.CompletedTask;
        }
    }
}
