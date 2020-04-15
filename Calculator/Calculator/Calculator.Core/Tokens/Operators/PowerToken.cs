using System;

namespace Calculator.Core.Tokens.Operators
{
    internal class PowerToken : OperatorToken
    {
        public PowerToken(char sign = '^') : base(sign, 3, Core.OperatorType.Power)
        {
        }

        public override double Apply(NumberToken a, NumberToken b)
        {
            return Math.Pow(a.GetValue(), b.GetValue());
        }
    }
}
