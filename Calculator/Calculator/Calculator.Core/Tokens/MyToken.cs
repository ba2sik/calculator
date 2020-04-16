using System.Collections.Generic;

namespace Calculator.Core.Tokens
{
    public abstract class Token
    {
        public TokenType Type { get; }
        public int Precedence { get; }

        protected Token(TokenType type, int precedence = 0)
        {
            Type = type;
            Precedence = precedence;
        }

        public abstract void PerformAlgorithmStep(ref Stack<Token> operators,
                                                  ref Queue<Token> output);
    }
}
