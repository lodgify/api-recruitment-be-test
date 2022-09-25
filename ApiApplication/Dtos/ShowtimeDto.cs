using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiApplication.Dtos
{
    public class ShowtimeDto: IValidatableObject
    {
        [Range(0, int.MaxValue)]
        public int Id { get; set; }

        public MovieDto Movie { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [RegularExpression(@"^(([0-1]?[0-9]|2[0-3]):[0-5][0-9](\s*\,)+\s*)*(([0-1]?[0-9]|2[0-3]):[0-5][0-9]\s*)$", ErrorMessage = "Invalid Schedule time format! Please enter correct format like: 12:30,04:00, 18:20")]
        public string Schedule { get; set; }
        
        [Required]
        [Range(1, int.MaxValue)]
        public int AuditoriumId { get; set; }
        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (this.StartDate > this.EndDate)
            {
                errors.Add(new ValidationResult("StartDate should be less than or equal to EndDate", new string[] { nameof(this.StartDate), nameof(this.EndDate) }));
            }

            return errors;
        }
    }
}
