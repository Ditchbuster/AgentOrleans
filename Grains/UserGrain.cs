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
        private StateType state;
        private Guid agentId;
        private List<string> userCommands;

        public UserGrain()
        {
            this.name = "test";
            this.state = StateType.NEW;
            this.agentId = Guid.NewGuid();
            this.userCommands = new List<string>();
        }

        public Task<string> GetUserName()
        {
            return Task.FromResult(this.name);
        }

        public Task<string> GetUserState()
        {
            return Task.FromResult(this.state.ToString());
        }

        public Task<string> GetInfo(string msg)
        {
            return Task.FromResult(msg);
        }

        public Task Action(string msg)
        {
            //TODO any logic to clean or reject the command.. 
            //userCommands.Append(msg); log the message somewhere at somepoint...

            return Task.CompletedTask;
        }

        public Task<string> GetAgentInfo(string agentId = null)
        {
            string agentGuid = agentId is null ? this.agentId.ToString() : agentId;
            return this.GrainFactory.GetGrain<AgentGrain>(agentGuid).GetAgentName();
        }

        public Task StartMission(int taskId, string agentId)
        {
            GrainFactory.GetGrain<ITaskGrain>(taskId).AddTaskEntity(agentId);
            GrainFactory.GetGrain<ITaskGrain>(taskId).StartTask();
            return Task.CompletedTask;
        }

        public enum StateType
        {
            NEW,
            INITIALIZED
        }
        public enum ActionType
        {

        }
        public enum InfoType
        {
            ALL,
            AGENT,
            STATE,
            MAP,
            TASK,
        }

    }
}
