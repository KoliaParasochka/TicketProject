using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class MyUser
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }

        public MyUser()
        {
            if(Tickets == null)
                Tickets = new List<Ticket>();
        }
    }
}
