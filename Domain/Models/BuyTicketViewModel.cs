using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class BuyTicketViewModel
    {
        public int ChosenTicketId { get; set; }
        public int VagonNumber { get; set; }

        public string RouteName { get; set; }
        
        public int TrainNumber { get; set; }

        public string VagonType { get; set; }
        public string Email { get; set; }
    }
}
