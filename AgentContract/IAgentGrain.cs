using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AgentContract
{
    /// <summary>
    /// The grain represents the agent inside the silo, ....
    /// </summary>

    public interface IAgentGrain : IGrainWithGuidKey
    {
        Task<AgentData> GetAgentData();
    }
}