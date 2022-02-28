using Cinema.Core.Entities;
using System;
using System.Collections.Generic;

namespace Cinema.Entities.Concrete
{
    public class ShowtimeEntity : IEntity
    {
        public int Id { get; set; }
        public MovieEntity Movie { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IEnumerable<string> Schedule { get; set; }
    }
}
