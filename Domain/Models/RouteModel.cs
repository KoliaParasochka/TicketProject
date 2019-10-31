using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class RouteModel
    {
        public int Id { get; set; }

        public virtual Train Train { get; set; }

        public virtual List<Station> Stations { get; set; }

        public RouteModel()
        {
            Stations = new List<Station>();
        }
    }
}
