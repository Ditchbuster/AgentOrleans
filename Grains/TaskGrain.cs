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
    public class TaskGrain : Grain, ITaskGrain, IRemindable
    {
        private HashSet<string> entities;
        private string name;
        private string status;
        private bool running;
        private int taskRemaining;

        public TaskGrain() => this.entities = new HashSet<string>();

        public Task<string> GetTaskName()
        {
            return Task.FromResult<string>(this.name);
        }
        public Task<string> GetTaskStatus()
        {
            return Task.FromResult<string>(this.name);
        }
        public Task StartTask()
        {
            if (!running)
            {
                this.running = true;
                this.taskRemaining = 5;
                RegisterOrUpdateReminder("reminder", TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1));
                return Task.CompletedTask;
            }
            return Task.FromException(new InvalidOperationException("Task is already running"));
        }
        public Task FinishTask()
        {
            this.running = false;
            this.UnregisterReminder(GetReminder("reminder").Result);
            return Task.CompletedTask;
        }
        public Task<int> AddTaskEntity(string entityId)
        {
            this.entities.Add(entityId);
            return Task.FromResult<int>(0);
        }

        Task IRemindable.ReceiveReminder(string reminderName, TickStatus status)
        {
            Console.WriteLine("TaskGrain reminder: " + reminderName);
            taskRemaining += -1;
            if (taskRemaining >= 0)
            {
                foreach (string entityId in entities)
                {
                    var agent = GrainFactory.GetGrain<IAgentGrain>(entityId);
                    agent.ChangeSkill(1);
                }
            }
            else
            {
                FinishTask();
                Console.WriteLine("Task completed");
            }
            return Task.CompletedTask;
        }

    }
}
