using System;
using System.Collections.Generic;

namespace ApiApplication.Models
{
    public class ShowTimeResponseModel
    {
        public int Id { get; set; }
        public MovieResponseModel Movie { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IEnumerable<string> Schedule { get; set; }
        public int AuditoriumId { get; set; }
    }
}
