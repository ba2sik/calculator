using Calculator.Core.Helper;
using System.Collections.Generic;

namespace Calculator.Core.Tokens.Operators
{
    public abstract class OperatorToken : MyToken
    {
        public readonly char sign;
        public readonly bool isLeftAssociative;
        public readonly OperatorTypes operatorType;

        protected OperatorToken(char sign,
                             int precedence,
                             OperatorTypes operatorType,
                             bool isLeftAssociative = false)
                                : base(TokenType.Operator, precedence)
        {
            this.sign = sign;
            this.isLeftAssociative = isLeftAssociative;
            this.operatorType = operatorType;
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
