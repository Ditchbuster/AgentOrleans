using System;
using Orleans.Concurrency;

namespace Contracts
{
    [Immutable]
    [Serializable]
    public class LocationData
    {
        public LocationData()
        { }

        public LocationData(Guid locationId, string name, int control, int coord, DateTime timestamp)
        {
            LocationId = locationId;
            Name = name;
            Control = control;
            Coord = coord;
            Timestamp = timestamp;
        }

        public Guid LocationId { get; set; }
        public string Name { get; set; }
        public int Control { get; set; }
        public int Coord { get; set; }
        public DateTime Timestamp { get; set; }
    }
}