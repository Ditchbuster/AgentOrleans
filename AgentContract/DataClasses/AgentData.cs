using System;
using Orleans.Concurrency;

namespace AgentContract
{
    [Serializable]
    public class AgentData
    {
        public Guid agentId { get; set; }
        public string? name { get; set; }
        public int skills { get; set; } //TODO make it a class
    }
}