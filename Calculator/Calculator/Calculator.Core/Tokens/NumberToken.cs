using System.Collections.Generic;

namespace Calculator.Core.Tokens
{
    public class NumberToken : Token
    {
        private string _value;

        public NumberToken(string str) : base(TokenType.Number)
        {
            _value = str;
        }

        public double GetValue()
        {
            return double.Parse(_value);
        }

        public void ConcatCharacter(char c)
        {
            _value += c.ToString();
        }

        public override void PerformAlgorithmStep(ref Stack<Token> operators, ref Queue<Token> output)
        {
            output.Enqueue(this);
        }
    }
}
