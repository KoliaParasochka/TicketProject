using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Services
{
    public class RouteInfo
    {
        public int Id { get; set; }
        public int EmptyPlaces { get; set; }
        public TimeSpan TravelTime { get; set; }
        
        public virtual Train Train { get; set; }

        public virtual List<Station> Stations { get; set; }

        public List<Vagon> Vagons { get; set; }

        public RouteInfo()
        {
            Stations = new List<Station>();
            Vagons = new List<Vagon>();
        }
    }
}