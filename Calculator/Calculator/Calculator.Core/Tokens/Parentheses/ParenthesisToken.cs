namespace Calculator.Core.Tokens.Parentheses
{
    public abstract class ParenthesisToken : MyToken
    {
        public readonly char sign;

        protected ParenthesisToken(char sign, TokenType type) : base(type, 4)
        {
            this.sign = sign;
        }
    }
}
