using System.Collections.Generic;

namespace Calculator.Core.Tokens
{
    public abstract class MyToken
    {
        public TokenType Type { get; }
        public int Precedence { get; }

        protected MyToken(TokenType type, int precedence = 0)
        {
            Type = type;
            Precedence = precedence;
        }

        public abstract void PerformAlgorithmStep(ref Stack<MyToken> operators,
                                                  ref Queue<MyToken> output);
    }
}
