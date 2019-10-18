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

        public virtual LinkedList<Station> Stations { get; set; }

        public List<Vagon> Vagons { get; set; }

        public RouteInfo()
        {
            Stations = new LinkedList<Station>();
            Vagons = new List<Vagon>();
        }
    }
}