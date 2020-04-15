namespace Calculator.Core.Exceptions
{
    public class IllegalOperationException : ParsingException
    {
        public IllegalOperationException(int index, string message) : base(index, message)
        {
        }
    }
}