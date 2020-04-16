using System.Collections.Generic;

namespace Calculator.Core.Tokens.Parentheses
{
    internal class LeftParenthesisToken : ParenthesisToken
    {
        public LeftParenthesisToken(char sign = '(') : base(sign, TokenType.LeftParenthesis)
        {
        }

        public override void PerformAlgorithmStep(ref Stack<Token> operators, ref Queue<Token> output)
        {
            operators.Push(this);
        }
    }
}
