using Calculator.Core.Tokens.Operators;
using System;

namespace Calculator.Core.Tokens.Factory
{
    internal static class OperatorTokenFactory
    {
        public static OperatorToken Create(OperatorTypes type)
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
                    throw new ArgumentException($"Type {type} doesn't exist");
            }

        }
    }
}
