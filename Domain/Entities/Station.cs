using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Station : IComparable<Station>
    {
        
        public int Id { get; set; }

        [Required]
        [Display(Name = "Название")]
        [RegularExpression(@"[A-Za-zА-Яа-я]+", ErrorMessage = "Имя не может содержать цыфры")]
        [StringLength(100, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Дата прибытия")]
        public DateTime ArrivingTime { get; set; }

        [Required]
        [Display(Name = "Дата отъезда")]
        public DateTime DepartureTime { get; set; }

        public int? RouteId { get; set; }
        public Route Route { get; set; }

        /// <summary>
        /// Comparing 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Station other)
        {
            return this.ArrivingTime.CompareTo(other.ArrivingTime);
        }
    }
}
