using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiApplication.DTOs
{
    public enum ResponseMessageType { Error = 3, Warning = 2, Information = 1 }

    public enum ResponseMessagePriority { Low = 1, Normal = 2, High = 3 }

    public class ResponseMessage
    {
        public string Text { get; set; }

        public string Description { get; set; }

        public ResponseMessageType Type { get; set; }

        public ResponseMessagePriority Priority { get; set; }

        public IEnumerable<string> InvalidMemberNames { get; set; }

        public IEnumerable<string> Errors { get; set; }

        public ResponseMessage(string text, string description, ResponseMessageType type, ResponseMessagePriority priority, IEnumerable<string> invalidMemberNames, IEnumerable<string> errors)
        {
            this.Text = text;
            this.Type = type;
            this.Priority = priority;
            this.Description = description;
            this.InvalidMemberNames = invalidMemberNames;
            this.Errors = errors;
        }

        public ResponseMessage(string text, string description, ResponseMessageType type, ResponseMessagePriority priority) : this(text, description, type, priority, new string[] { }, new string[] { })
        {
        }
    }
}
