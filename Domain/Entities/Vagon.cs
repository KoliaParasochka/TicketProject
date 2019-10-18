
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Vagon
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int Places { get; set; }
        public int EmptyPlaces { get; set; }
        public int BusyPaces { get; set; }
        public string Type { get; set; }

        public int? TrainId { get; set; }
        public virtual Train Train { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }

        public Vagon()
        {
            if(Tickets == null)
                Tickets = new List<Ticket>();
        }
    }
}
