using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ILocationGrain : IGrainWithStringKey
    {
        Task<string> GetLocationName();
        //Task<string> GetCordinate();
        Task<string> GetTasks();
        Task<string> ToggleActivity();
    }
}