using System.Collections.Generic;

namespace Calculator.Core.Tokens.Parentheses
{
    internal class RightParenthesisToken : ParenthesisToken
    {
        public RightParenthesisToken(char sign = ')') : base(sign, TokenType.RightParenthesis)
        {
        }

        public override void PerformAlgorithmStep(ref Stack<Token> operators, ref Queue<Token> output)
        {
            while (!(operators.Peek() is LeftParenthesisToken))
            {
                output.Enqueue(operators.Pop());
            }
            // Getting rid of the left parenthesis
            operators.Pop();
        }
    }
}
