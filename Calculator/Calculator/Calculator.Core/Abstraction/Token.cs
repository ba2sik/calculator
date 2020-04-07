namespace Calculator.Core.Abstraction
{
    public abstract class Token
    {
        public TokenTypes type;
        public string value;

        protected Token(TokenTypes type, string value)
        {
            this.type = type;
            this.value = value;
        }
    }
}
