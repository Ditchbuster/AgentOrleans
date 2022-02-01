using Contracts;
using Orleans;
using Orleans.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Grains
{

    public class TaskGrain : Grain, ITaskGrain, IRemindable
    {
        private readonly ILogger _logger;

        private HashSet<string> entities;
        private string name;
        private string status;
        private bool running;
        private int taskRemaining;
        private List<Guid> taskLocations; // eventually a dict or object to hold more complex locations, task types, time lengths?

        public TaskGrain(ILogger<TaskGrain> logger)
        {
            _logger = logger;
            this.entities = new HashSet<string>();
            this.running = false;
        }

        public Task<string> GetTaskName()
        {
            return Task.FromResult<string>(this.name);
        }
        public Task<string> GetTaskStatus()
        {
            return Task.FromResult<string>(this.status);
        }
        public Task StartTask()
        {
            if (taskLocations == null)
            { //just for testing create random location GUID
                this.taskLocations = new List<Guid>(5);
                this.taskLocations.Add(Guid.NewGuid());
            }
            if (!running)
            {

                this.running = true;
                this.taskRemaining = 5;
                var grain = this.GrainFactory.GetGrain<ILocationGrain>(this.taskLocations[0]);
                grain.ChangeStats(1);
                _logger.LogInformation("We made it here");
                RegisterOrUpdateReminder("reminder", TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1));
                return Task.CompletedTask;
            }
            return Task.FromException(new InvalidOperationException("Task is already running"));
        }
        public Task FinishTask()
        {
            this.running = false;
            this.GrainFactory.GetGrain<ILocationGrain>(this.taskLocations[0]).ChangeStats(-1);
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
            _logger.LogInformation("TaskGrain reminder: " + reminderName);
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
                _logger.LogInformation("Task completed");
            }
            return Task.CompletedTask;
        }

    }
}
