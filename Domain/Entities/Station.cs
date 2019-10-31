using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Station : IComparable<Station>, IValidatableObject
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

        /// <summary>
        /// Checking input data
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            if(this.ArrivingTime > this.DepartureTime)
            {
                errors.Add(new ValidationResult("Время отбытия должно быть больше чем время прибытия"));
            }
            if (this.ArrivingTime < DateTime.Now || this.DepartureTime < DateTime.Now)
            {
                errors.Add(new ValidationResult("Нельзя установить дату ниже нынишней!"));
            }

            return errors;
        }
    }
}
