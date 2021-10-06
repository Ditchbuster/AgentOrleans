using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    /// <summary>
    /// The grain represents the user inside the silo, coordinates with clients outside the silo, receive and dispatch actions for the user
    /// </summary>

    public interface IUserGrain : IGrainWithGuidKey
    {
        Task<string> GetUserName();
        //public Guid GameUserId { get; set; }
        Task<string> GetUserState();
        Task QueueCommand(string msg);
        //Task<string> AddUserCommand(string msg);
        public Task StartMission(int taskId, string agentId); // dummy

    }
}