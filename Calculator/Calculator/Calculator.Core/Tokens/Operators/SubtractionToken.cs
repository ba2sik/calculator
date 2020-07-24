﻿namespace Calculator.Core.Tokens.Operators
{
    public class SubtractionToken : OperatorToken
    {
        public SubtractionToken(char sign = '-') : base(sign, 1, OperatorType.Subtraction)
        {
        }

        public override double Apply(NumberToken a, NumberToken b)
        {
            return a.GetValue() - b.GetValue();
        }
    }
}
