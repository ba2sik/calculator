namespace Calculator.Core.Tokens.Operators
{
    internal class DivisionToken : OperatorToken
    {
        public DivisionToken(char sign = '/') : base(sign, 2, OperatorTypes.Division)
        {
        }

        public override double Apply(NumberToken a, NumberToken b)
        {
            return a.GetValue() / b.GetValue();
        }
    }
}
