namespace Calculator.Core.Tokens.Operators
{
    internal class SubtractionToken : OperatorToken
    {
        public SubtractionToken(char sign = '-') : base(sign, 1, OperatorTypes.Subtraction)
        {
        }

        public override double Apply(NumberToken a, NumberToken b)
        {
            return a.GetValue() - b.GetValue();
        }
    }
}
