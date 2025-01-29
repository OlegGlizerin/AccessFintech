using System;

namespace AccessFintech.Exceptions
{
    public class CsvNotFoundException : Exception
    {
        public CsvNotFoundException() { }

        public CsvNotFoundException(string message) : base(message) { }

        public CsvNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}
