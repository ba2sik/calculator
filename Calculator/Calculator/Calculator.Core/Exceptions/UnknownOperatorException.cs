namespace Calculator.Core.Exceptions
{
    public class UnknownOperatorException : ParsingException
    {
        public UnknownOperatorException(int index, string op)
            : base(index, $"Unknown operator: {op}")
        {
        }
    }
}
