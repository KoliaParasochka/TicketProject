using System;
using System.Collections.Generic;


namespace Domain.Entities
{
    public class Route
    {
        public int Id { get; set; }

        public virtual Train Train { get; set; }

        public virtual List<Station> Stations { get; set; }

        public Route ()
        {
            Stations = new List<Station>();
        }
    }
}
