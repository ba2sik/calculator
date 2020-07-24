using System;

namespace Calculator.Core.Exceptions
{
    public class TokenizationException : Exception
    {
        public TokenizationException(int index, string message)
                : base($"Tokenization error at index: {index}. {message}")
        {
        }
    }
}