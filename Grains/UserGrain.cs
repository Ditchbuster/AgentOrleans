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

        public UserGrain(string name, StateType state = StateType.NEW)
        {
            this.name = name;
            this.state = state;
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
            if (msg == "start")
            {
                //tell location ive started
            }
            else
            {
                //tell location ive stopped
            }
            return Task.CompletedTask;
        }

        public Task<string> GetAgentInfo(string agentId = null)
        {
            string agentGuid = agentId is null ? this.agentId.ToString() : agentId;
            return this.GrainFactory.GetGrain<AgentGrain>(agentGuid).GetAgentName();
        }

        public Task StartMission(string taskId, string agentId)
        {
            GrainFactory.GetGrain<ITaskGrain>(Guid.Parse(taskId)).AddTaskEntity(agentId);
            GrainFactory.GetGrain<ITaskGrain>(Guid.Parse(taskId)).StartTask();
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
