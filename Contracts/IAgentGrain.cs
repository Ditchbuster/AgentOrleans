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
        Task<string> ToggleActivity2();
        Task ChangeSkill(int amount);
    }
}