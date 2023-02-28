using System.Collections.Generic;
using System;

namespace ApiApplication.Models.ViewModels
{
    public class ScheduleViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IEnumerable<string> Schedule
        {
            get; set;
        }
    }
}