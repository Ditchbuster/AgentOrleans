using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AgentContract
{
    /// <summary>
    /// The grain represents the user inside the silo, coordinates with clients outside the silo, receive and dispatch actions for the user
    /// </summary>

    public interface IUserGrain : IGrainWithGuidKey
    {
        Task<UserData> GetUserData();
        Task<AgentData> GetAgentData();
        Task<List<AgentData>> GetAgentDataList();
        Task<TaskData> GetTaskData();
    }
}