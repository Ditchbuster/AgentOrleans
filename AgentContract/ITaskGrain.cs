using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AgentContract
{
    /// <summary>
    /// The grain represents a task inside the silo, ....
    /// </summary>

    public interface ITaskGrain : IGrainWithGuidKey
    {
        Task<TaskData> GetTaskData();
        // TaskDetail?? or just part of taskdata
        Task<Guid> PutTaskData(TaskData data);
    }
}