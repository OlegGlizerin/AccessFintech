using System;

namespace AccessFintech.Exceptions
{
    public class JsonNotFoundException : Exception
    {
        public JsonNotFoundException() { }

        public JsonNotFoundException(string message) : base(message) { }

        public JsonNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}
