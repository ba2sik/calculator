using Calculator.Core.Abstraction;

namespace Calculator.Core.Implementation
{
    internal class CalculatorToken : Token
    {
        public CalculatorToken(TokenTypes type, string value) : base(type, value) { }

        public override bool Equals(object obj)
        {
            return this.value == (obj as CalculatorToken).value &&
                    this.type == (obj as CalculatorToken).type;
        }


        public override int GetHashCode()
        {
            // Example I found on the internet
            int hash = 13;
            hash = (hash * 7) + value.GetHashCode();
            hash = (hash * 7) + type.GetHashCode();
            return hash;
        }
    }
}
