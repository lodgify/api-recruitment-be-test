using System;
using System.Collections.Generic;

namespace ApiApplication.DTO
{
    public class ScheduleDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IEnumerable<string> Schedule { get; set; }


    }
}
