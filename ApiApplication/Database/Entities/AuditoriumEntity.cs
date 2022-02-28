﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiApplication.Database.Entities
{
    public class AuditoriumEntity
    {
        public int Id { get; set; }
        public IEnumerable<ShowtimeEntity> Showtimes { get; set; }
        public int Seats { get; set; }
       
    }
}
