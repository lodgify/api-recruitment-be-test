using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiApplication.Dtos
{
    public class MovieDto : IValidatableObject
    {
        public string Title { get; set; }

        [Required]
        public string ImdbId { get; set; }
        
        public string Stars { get; set; }
        
        public DateTime ReleaseDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            return errors;
        }
    }
}
