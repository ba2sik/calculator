using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Core.Tokens
{
    internal class SubtractionToken : OperatorToken
    {
        public SubtractionToken(char sign = '-') : base(sign)
        {
        }

        public override double Apply(NumberToken a, NumberToken b)
        {
            return a.value - b.value;
        }
    }
}
