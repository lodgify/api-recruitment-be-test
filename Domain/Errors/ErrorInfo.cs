﻿using System.Text.Json;

namespace Domain.Errors
{
    public class ErrorInfo
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
