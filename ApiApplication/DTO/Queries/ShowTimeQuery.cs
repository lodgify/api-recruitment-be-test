using System;

namespace ApiApplication.DTO.Queries {
    public class ShowTimeQuery {
        public DateTime? StartDate { get; set; }
        public string MovieTitle { get; set; }
    }
}
