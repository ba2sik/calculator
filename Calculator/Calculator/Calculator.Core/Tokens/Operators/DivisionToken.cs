namespace Calculator.Core.Tokens.Operators
{
    public class DivisionToken : OperatorToken
    {
        public DivisionToken(char sign = '/') : base(sign, 2, OperatorType.Division)
        {
        }

        public override double Apply(NumberToken a, NumberToken b)
        {
            return a.GetValue() / b.GetValue();
        }
    }
}
