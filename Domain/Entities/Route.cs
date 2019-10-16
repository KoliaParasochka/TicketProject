using System;
using System.Collections.Generic;


namespace Domain.Entities
{
    public class Route
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Train Train { get; set; }

        public Station StartStation { get; set; }
        public Station FinishStation { get; set; }

        public virtual ICollection<Station> Stations { get; set; }

        public Route ()
        {
            Stations = new List<Station>();
        }
    }
}
