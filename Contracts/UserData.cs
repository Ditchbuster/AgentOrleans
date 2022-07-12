using System;
using Orleans.Concurrency;

namespace Contracts
{
    [Serializable]
    public class UserData
    {
        /* public UserData()
        { }

        public UserData(Guid userId, string name)
        {
            this.userId = userId;
            this.name = name;
        } */

        public Guid userId { get; set; }
        public string name { get; set; }
    }
}