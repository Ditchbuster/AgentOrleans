using System;
using Orleans.Concurrency;

namespace AgentContract
{
    [Serializable]
    public class AgentData
    {
        public Guid agentId { get; set; }
        public string name { get; set; }
        public int skills { get; set; } //TODO make it a class

        public AgentData()
        {
            agentId = Guid.NewGuid();
            name = "Agent:" + agentId.ToString();
            skills = 0;
            //AgentData(Guid.NewGuid(),)
        }
        public AgentData(Guid agentId, String name)
        {
            this.agentId = agentId;
            this.name = name;
            this.skills = 0;
        }
        public AgentData(Guid agentId, String name, int skills)
        {
            this.agentId = agentId;
            this.name = name;
            this.skills = skills;
        }

    }
}