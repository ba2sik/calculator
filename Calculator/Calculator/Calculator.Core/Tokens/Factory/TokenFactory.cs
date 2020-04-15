using Calculator.Core.Tokens.Operators;
using Calculator.Core.Tokens.Parentheses;
using System;

namespace Calculator.Core.Tokens
{
    internal static class TokenFactory
    {
        public static MyToken Create(TokenTypes type, char c = '\0')
        {
            switch (type)
            {
                case TokenTypes.Number:
                    return new NumberToken(c.ToString());
                case TokenTypes.LeftParenthesis:
                    return new LeftParenthesisToken();
                case TokenTypes.RightParenthesis:
                    return new RightParenthesisToken();
                default:
                    throw new ArgumentException($"Unknown type: {type}");
            }
        }

        public static MyToken Create(OperatorTypes type)
        {
            switch (type)
            {
                case OperatorTypes.Addition:
                    return new AdditionToken();
                case OperatorTypes.Subtraction:
                    return new SubtractionToken();
                case OperatorTypes.Multiplication:
                    return new MultiplicationToken();
                case OperatorTypes.Division:
                    return new DivisionToken();
                case OperatorTypes.Power:
                    return new PowerToken();
                default:
                    throw new ArgumentException($"Unknown type: {type}");
            }

        }
    }
}
