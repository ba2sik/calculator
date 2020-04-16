using Calculator.Core.Tokens.Operators;
using Calculator.Core.Tokens.Parentheses;
using System;

namespace Calculator.Core.Tokens.Factory
{
    internal static class TokenFactory
    {
        public static Token Create(TokenType type, char c = '\0')
        {
            switch (type)
            {
                case TokenType.Number:
                    return new NumberToken(c.ToString());
                case TokenType.LeftParenthesis:
                    return new LeftParenthesisToken();
                case TokenType.RightParenthesis:
                    return new RightParenthesisToken();
                default:
                    throw new ArgumentException($"Unknown type: {type}");
            }
        }

        public static Token Create(OperatorType type)
        {
            switch (type)
            {
                case OperatorType.Addition:
                    return new AdditionToken();
                case OperatorType.Subtraction:
                    return new SubtractionToken();
                case OperatorType.Multiplication:
                    return new MultiplicationToken();
                case OperatorType.Division:
                    return new DivisionToken();
                case OperatorType.Power:
                    return new PowerToken();
                default:
                    throw new ArgumentException($"Unknown operator type: {type}");
            }

        }
    }
}
