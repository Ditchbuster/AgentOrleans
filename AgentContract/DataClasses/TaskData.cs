using System;
using Orleans.Concurrency;

namespace AgentContract
{
    [Serializable]
    public class TaskData
    {
        public Guid taskId { get; set; }
        public string name { get; set; }
        public List<Guid> assignedAgents { get; set; }

        public TaskData()
        {
            taskId = Guid.NewGuid();
            name = "Task: " + taskId.ToString();
            assignedAgents = new List<Guid>();
        }
        public TaskData(Guid taskId, String name)
        {
            this.taskId = taskId;
            this.name = name;
            this.assignedAgents = new List<Guid>();
        }
        public TaskData(Guid taskId, String name, List<Guid> assignedAgents)
        {
            this.taskId = taskId;
            this.name = name;
            this.assignedAgents = new List<Guid>();
        }

    }
}