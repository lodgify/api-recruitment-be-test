using System.Collections.Generic;
using System.Linq;

namespace ApiApplication.Models
{
    public class ResponseModel<T>
    {
        public bool Success => !Errors.Any();

        public List<string> Errors { get; set; }
        public List<string> Messages { get; set; }

        public T Result { get; set; }

        public int StatusCode { get; set; }

        public ResponseModel()
        {
            Errors = new List<string>();
            Messages = new List<string>();
        }

        public ResponseModel<T> NotFound()
        {
            StatusCode = 404;
            return this;
        }

        public ResponseModel<T> InternalServerError()
        {
            StatusCode = 500;
            return this;
        }
        public ResponseModel<T> BadRequest()
        {
            StatusCode = 400;
            return this;
        }

        public ResponseModel<T> Ok(object data)
        {
            StatusCode = 200;
            Result = (T)data;

            return this;
        }

        public void AddError(string error)
        {
            Errors.Add(error);
        }
        public void AddMessage(string message)
        {
            Messages.Add(message);
        }
        public void AddErrors(Dictionary<string, List<string>> errors)
        {
            foreach (var error in errors)
            {
                AddError(error.Value.ToString());
            }
        }
    }
}