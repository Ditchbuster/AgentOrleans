using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ITaskGrain : IGrainWithGuidKey
    {
        Task<string> GetTaskName();
        Task<string> GetTaskStatus();

        Task StartTask();
        Task FinishTask();

        Task<int> AddTaskEntity(string entityId);
    }
}