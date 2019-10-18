using System;
using System.Collections.Generic;


namespace Domain.Entities
{
    public class Route
    {
        public int Id { get; set; }

        public virtual Train Train { get; set; }

        public virtual LinkedList<Station> Stations { get; set; }

        public Route ()
        {
            if(Stations == null)
                Stations = new LinkedList<Station>();
        }
    }
}
