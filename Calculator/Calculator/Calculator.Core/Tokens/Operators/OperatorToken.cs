using Calculator.Core.Helper;
using System.Collections.Generic;

namespace Calculator.Core.Tokens.Operators
{
    public abstract class OperatorToken : MyToken
    {
        public char Sign { get; }
        public bool IsLeftAssociative { get; }
        public OperatorType OperatorType { get; }

        protected OperatorToken(char sign,
                             int precedence,
                             OperatorType operatorOperatorType,
                             bool isLeftAssociative = false)
                                : base(TokenType.Operator, precedence)
        {
            Sign = sign;
            IsLeftAssociative = isLeftAssociative;
            OperatorType = operatorOperatorType;
        }

        public abstract double Apply(NumberToken a, NumberToken b);

        public override void PerformAlgorithmStep(ref Stack<MyToken> operators,
                                                  ref Queue<MyToken> output)
        {
            while (MyHelper.OperatorAtTop(operators))
            {
                var op = operators.Peek() as OperatorToken;

                if (MyHelper.ShouldPopOperator(this, op))
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
    }
}
