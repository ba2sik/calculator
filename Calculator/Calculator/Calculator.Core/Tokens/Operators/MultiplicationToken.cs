namespace Calculator.Core.Tokens.Operators
{
    internal class MultiplicationToken : OperatorToken
    {
        public MultiplicationToken(char sign = '*') : base(sign, 2, OperatorType.Multiplication)
        {
        }

        public override double Apply(NumberToken a, NumberToken b)
        {
            return a.GetValue() * b.GetValue();
        }
    }
}
