using System;

namespace ApiApplication.Models.ViewModels
{
    public class ShowtimeViewModel
    {
        public int Id { get; set; }
        public DateTime Start_date { get; set; }
        public DateTime End_date { get; set; }
        public MovieViewModel Movie { get; set; }
        public int Auditorium_id { get; set; }
        public string[] Schedule { get; set; }
    }
}
