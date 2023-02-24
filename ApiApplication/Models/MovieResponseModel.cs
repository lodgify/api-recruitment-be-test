﻿using System;

namespace ApiApplication.Models
{
    public class MovieResponseModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImdbId { get; set; }
        public string Stars { get; set; }
        public DateTime ReleaseDate { get; set; }

        public int ShowtimeId { get; set; }
    }
}
