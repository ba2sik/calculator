using System;

namespace Calculator.Core.Tokens.Operators
{
    public class PowerToken : OperatorToken
    {
        public PowerToken(char sign = '^') : base(sign, 3, OperatorType.Power, false)
        {
        }

        public override double Apply(NumberToken a, NumberToken b)
        {
            return Math.Pow(a.GetValue(), b.GetValue());
        }
    }
}
