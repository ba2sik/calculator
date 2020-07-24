using System;
using Calculator.Core.Helpers;
using System.Collections.Generic;

namespace Calculator.Core.Tokens.Operators
{
    public abstract class OperatorToken : Token
    {
        public char Sign { get; }
        public bool IsLeftAssociative { get; }
        public OperatorType OperatorType { get; }

        protected OperatorToken(char sign,
                             int precedence,
                             OperatorType operatorOperatorType,
                             bool isLeftAssociative = true)
                                : base(TokenType.Operator, precedence)
        {
            Sign = sign;
            IsLeftAssociative = isLeftAssociative;
            OperatorType = operatorOperatorType;
        }

        public abstract double Apply(NumberToken a, NumberToken b);

        public override void PerformAlgorithmStep(ref Stack<Token> operators,
                                                  ref Queue<Token> output)
        {
            while (ShuntingYardHelper.OperatorAtTop(operators))
            {
                var op = operators.Peek() as OperatorToken;

                if (ShuntingYardHelper.ShouldPopOperator(this, op))
                {
                    output.Enqueue(operators.Pop());
                }
                else
                {
                    break;
                }
            }

            operators.Push(this);
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;

            OperatorToken other = obj as OperatorToken;
            if ((Object)other == null)
                return false;

            // here you need to compare two objects
            // below is just example implementation

            return this.OperatorType == other.OperatorType;
        }
    }
}
