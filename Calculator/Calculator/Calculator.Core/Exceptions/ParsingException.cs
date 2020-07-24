using System;

namespace Calculator.Core.Exceptions
{
    public class ParsingException : Exception
    {
        public ParsingException(string message)
            : base($"Parsing error: {message}.")
        {
        }

        public ParsingException(int index, string message)
                : base($"Parsing error at index: {index}. {message}")
        {
        }
    }
}