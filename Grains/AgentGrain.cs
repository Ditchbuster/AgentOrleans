using Contracts;
using Orleans;
using Orleans.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grains
{
    public class AgentGrain : Grain, IAgentGrain, IRemindable
    {
        private string name;
        private int skill;


        public AgentGrain()
        {
            this.name = "test";
            this.skill = 0;
        }

        public Task<string> GetAgentName()
        {
            return Task.FromResult(this.name);
        }

        public Task<int> GetAgentSkill()
        {
            return Task.FromResult(this.skill);
        }

        public Task<string> ToggleActivity()
        {
            RegisterOrUpdateReminder("reminder", TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1));
            return Task.FromResult("test return of toggleactivity");
        }

        public Task<string> ToggleActivity2()
        {
            RegisterOrUpdateReminder("reminder2", TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1));
            return Task.FromResult("test return of toggleactivity");
        }

        public Task ChangeSkill(int amount)
        {
            this.skill += amount;
            return Task.CompletedTask;
        }

        Task IRemindable.ReceiveReminder(string reminderName, TickStatus status)
        {
            Console.WriteLine("Reminder: " + reminderName);
            return Task.CompletedTask;
        }

    }
}
