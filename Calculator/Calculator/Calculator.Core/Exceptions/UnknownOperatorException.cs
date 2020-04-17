namespace Calculator.Core.Exceptions
{
    public class UnknownOperatorException : TokenizationException
    {
        public UnknownOperatorException(int index, string op)
            : base(index, $"Unknown operator: {op}")
        {
        }
    }
}
