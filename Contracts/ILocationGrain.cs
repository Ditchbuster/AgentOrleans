using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ILocationGrain : IGrainWithGuidKey
    {
        Task<string> GetLocationName();
        Task<int> GetCordinate();
        Task<int> GetControlLevel();
        Task SetControlLevel(int lvl);
        Task ChangeStats(int stats);
        Task<LocationData> GetData();
        Task<string> ToggleActivity();
    }
}