using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class RouteViewModel
    {
        public int Id { get; set; }
        public int TrainNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public string StartStation { get; set; }
        public string FinishStation { get; set; }
        public TimeSpan TravelTime { get; set; }

        public bool IsRemoved { get; set; }

        public List<Station> Stations { get; set; }

        public RouteViewModel()
        {
            Stations = new List<Station>();
        }
    }
}
