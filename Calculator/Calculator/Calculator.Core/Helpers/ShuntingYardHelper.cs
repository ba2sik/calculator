using Calculator.Core.Tokens;
using Calculator.Core.Tokens.Operators;
using System.Collections.Generic;
using System.Linq;

namespace Calculator.Core.Helpers
{
    internal class ShuntingYardHelper : Helper
    {
        public ShuntingYardHelper(
            List<OperatorToken> operators,
            ExpressionSymbols symbols) : base(operators, symbols)
        {
        }

        public static bool OperatorAtTop(Stack<Token> s)
        {
            return s.Any() && s.Peek() is OperatorToken;
        }

        public static bool ShouldPopOperator(OperatorToken a, OperatorToken b)
        {
            return a.IsLeftAssociative
                       ? a.Precedence <= b.Precedence
                       : a.Precedence < b.Precedence;
        }
    }
}