namespace Calculator.Core.Tokens.Parentheses
{
    public abstract class ParenthesisToken : Token
    {
        public char Sign { get; }

        protected ParenthesisToken(char sign, TokenType type) : base(type, 4)
        {
            Sign = sign;
        }
    }
}
