using System.Collections.Generic;

namespace Calculator.Core.Tokens.Parentheses
{
    internal class LeftParenthesisToken : ParenthesisToken
    {
        public LeftParenthesisToken(char sign = '(') : base(sign, TokenTypes.LeftParenthesis)
        {
        }

        public override void PerformAlgorithmStep(ref Stack<MyToken> operators, ref Queue<MyToken> output)
        {
            operators.Push(this);
        }
    }
}
