using System;

namespace Calculator.Core.Exceptions
{
    public class ParsingException : Exception
    {
        public ParsingException(int index, string message)
                : base($"Parsing Error at index: {index}. {message}")
        {
        }
    }
}