namespace Calculator.Core.Tokens.Operators
{
    public class AdditionToken : OperatorToken
    {
        public AdditionToken(char sign = '+') : base(sign, 1, OperatorType.Addition)
        {
        }

        public override double Apply(NumberToken a, NumberToken b)
        {
            return a.GetValue() + b.GetValue();
        }
    }
}
