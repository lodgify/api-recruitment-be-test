using System;

namespace ApiApplication.Auth
{
    public class ReadTokenException : Exception
    {
        public ReadTokenException(string encodedValue, Exception innerException) : base("Error while reading the Token", innerException)
        {
            EncodedValue = encodedValue;
            this.Data[nameof(EncodedValue)] = encodedValue;
        }
        public string EncodedValue { get; set; }
    }
}
