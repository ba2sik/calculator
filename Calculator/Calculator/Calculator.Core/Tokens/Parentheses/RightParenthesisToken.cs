using Calculator.Core.Exceptions;
using System.Collections.Generic;
using System.Linq;

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

            if (!operators.Any())
            {
                throw new ParsingException(
                    "Expression contains mismatched parentheses");
            }

            // popping the left parenthesis
            operators.Pop();
        }
    }
}
