using AgentContract;
using Orleans;
using Orleans.Runtime;

namespace Grains
{
    public class AgentGrain : Grain, IAgentGrain
    {
        private string name { get; set; }
        private int skills { get; set; }
        public AgentGrain()
        {
            this.name = this.GetPrimaryKey().ToString();
            this.skills = 0;
        }
        public Task<AgentData> GetAgentData()
        {
            return Task<AgentData>.FromResult(new AgentData(this.GetPrimaryKey(), name, skills));
        }
    }
}