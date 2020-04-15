using System.Collections.Generic;

namespace Calculator.Core.Tokens
{
    public abstract class MyToken
    {
        public readonly TokenTypes type;
        public readonly int precedence;

        protected MyToken(TokenTypes type, int precedence = 0)
        {
            this.type = type;
            this.precedence = precedence;
        }

        public abstract void PerformAlgorithmStep(ref Stack<MyToken> operators,
                                                  ref Queue<MyToken> output);
    }
}
