
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Vagon
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Номер вагона")]
        public int Number { get; set; }

        [Required]
        [Display(Name = "Количество мест")]
        public int Places { get; set; }
        public int EmptyPlaces { get; set; }
        public int BusyPaces { get; set; }

        [Required]
        [Display(Name = "Тип вагона")]
        [RegularExpression(@"[A-Za-zА-Яа-я]+", ErrorMessage = "Имя не может содержать цыфры")]
        [StringLength(100, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 3)]
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
