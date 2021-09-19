using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IUserGrain : IGrainWithIntegerKey
    {
        Task<string> GetUserName();
        //Task<string> GetUserID();
        Task<string> GetUserState();

        //Task<string> AddUserCommand(string msg);
        public Task StartMission(int taskId, string agentId); // dummy
    }
}