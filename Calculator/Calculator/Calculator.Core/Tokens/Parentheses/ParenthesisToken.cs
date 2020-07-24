using System;
using Calculator.Core.Tokens.Operators;

namespace Calculator.Core.Tokens.Parentheses
{
    public abstract class ParenthesisToken : Token
    {
        public char Sign { get; }

        protected ParenthesisToken(char sign, TokenType type) : base(type, 4)
        {
            Sign = sign;
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;

            ParenthesisToken other = obj as ParenthesisToken;
            if ((Object)other == null)
                return false;

            // here you need to compare two objects
            // below is just example implementation

            return this.Sign == other.Sign;
        }
    }
}
