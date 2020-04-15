﻿namespace Calculator.Core.Tokens.Operators
{
    internal class AdditionToken : OperatorToken
    {
        public AdditionToken(char sign = '+') : base(sign, 1, Core.OperatorType.Addition)
        {
        }

        public override double Apply(NumberToken a, NumberToken b)
        {
            return a.GetValue() + b.GetValue();
        }
    }
}
