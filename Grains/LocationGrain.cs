using Contracts;
using Orleans;
using Orleans.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grains
{
    public class LocationGrain : Grain, ILocationGrain, IRemindable
    {
        private string name;
        private int control; //TODO add multiuple factions, tracking each individually
        private int coord; //TODO change to object representing location in game world
        public LocationGrain()
        {
            this.name = "test";
            this.control = 0;
            this.coord = 0;
        }

        public Task<string> GetLocationName()
        {
            return Task.FromResult(this.name);
        }

        public Task<int> GetControlLevel()
        {
            return Task.FromResult(this.control);
        }

        public Task<int> GetCordinate()
        {
            return Task.FromResult(this.coord);
        }

        public Task<string> ToggleActivity()
        {
            RegisterOrUpdateReminder("reminder", TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1));
            return Task.FromResult("test return of toggleactivity");
        }

        public Task<string> ToggleActivity2()
        {
            RegisterOrUpdateReminder("reminder2", TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1));
            return Task.FromResult("test return of toggleactivity");
        }

        Task IRemindable.ReceiveReminder(string reminderName, TickStatus status)
        {
            Console.WriteLine("Reminder: " + reminderName);
            return Task.CompletedTask;
        }

    }
}
