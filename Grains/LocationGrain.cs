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
        private int changeStats;
        private int coord; //TODO change to object representing location in game world
        public LocationGrain()
        {
            this.name = "test";
            this.control = 0;
            this.changeStats = 0;
            this.coord = 0;
        }

        public override async Task OnActivateAsync()
        {
            this.GetPrimaryKey(out var stock);

            RegisterTimer(Tick, null, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1));

            await base.OnActivateAsync();
        }

        private Task Tick(object stock)
        {
            control += changeStats;
            return Task.CompletedTask;
        }

        public Task<string> GetLocationName()
        {
            return Task.FromResult(this.name);
        }

        public Task<int> GetControlLevel()
        {
            return Task.FromResult(this.control);
        }

        public Task SetControlLevel(int lvl)
        {
            this.control = lvl;
            return Task.CompletedTask;
        }

        public Task<LocationData> GetData()
        {
            return Task.FromResult(new LocationData(this.GetPrimaryKey(), name, control, coord, System.DateTime.Now));
        }
        public Task<int> GetCordinate()
        {
            return Task.FromResult(this.coord);
        }

        public Task ChangeStats(int stats) // TODO stats eventually a list of stats that effect the location 
        {
            this.changeStats += stats;
            return Task.CompletedTask;
        }

        public Task<string> ToggleActivity()
        {
            RegisterOrUpdateReminder("reminder", TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1));
            return Task.FromResult("test return of toggleactivity");
        }

        Task IRemindable.ReceiveReminder(string reminderName, TickStatus status)
        {
            Console.WriteLine("Reminder: " + reminderName);
            return Task.CompletedTask;
        }

    }
}
