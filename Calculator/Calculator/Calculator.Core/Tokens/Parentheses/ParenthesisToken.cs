namespace Calculator.Core.Tokens.Parentheses
{
    public  abstract class ParenthesisToken : MyToken
    {
        public readonly char sign;

        public ParenthesisToken(char sign, TokenTypes type) : base(type, precedence:4)
        {
            this.sign = sign;
        }
    }
}
