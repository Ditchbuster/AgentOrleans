using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IAgentGrain : IGrainWithStringKey
    {
        Task<string> GetAgentName();
        Task<int> GetAgentSkill();
        Task<string> ToggleActivity();
        Task ChangeSkill(int amount);
        //Task<AgentData> GetAgentData();
    }
}