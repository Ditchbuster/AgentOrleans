using System;
using Orleans.Concurrency;

namespace Contracts
{
    [Serializable]
    public class AgentData
    {
        /* public AgentData()
        { }

        public AgentData(Guid agentId, string name)
        {
            this.agentId = agentId;
            this.name = name;
        } */

        public Guid agentId { get; set; }
        public string name { get; set; }
    }
}