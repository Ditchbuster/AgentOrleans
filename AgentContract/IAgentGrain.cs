using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AgentContract
{
    /// <summary>
    /// The grain represents the user inside the silo, coordinates with clients outside the silo, receive and dispatch actions for the user
    /// </summary>

    public interface IAgentGrain : IGrainWithGuidKey
    {
        Task<AgentData> GetAgentData();
    }
}