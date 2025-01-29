using System;

namespace AccessFintech.Exceptions
{
    public class StockApiClientException : Exception
    {
        public StockApiClientException() { }

        public StockApiClientException(string message) : base(message) { }

        public StockApiClientException(string message, Exception innerException) : base(message, innerException) { }
    }
}
