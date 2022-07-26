using System;
using Orleans.Concurrency;

namespace AgentContract
{
    [Serializable]
    public class UserData
    {
        public Guid UserId { get; set; }
        public string? name { get; set; }
    }
}