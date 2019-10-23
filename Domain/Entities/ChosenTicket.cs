using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ChosenTicket
    {
        public int Id { get; set; }
        public int RouteId { get; set; }
        public int VagonId { get; set; }
        public int TrainId { get; set; }
        public string UserEmail { get; set; }
    }
}
