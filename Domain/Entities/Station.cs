using System;

namespace Domain.Entities
{
    public class Station
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ArrivingTime { get; set; }
        public DateTime DepartureTime { get; set; }

        public Route Route { get; set; }
    }
}
