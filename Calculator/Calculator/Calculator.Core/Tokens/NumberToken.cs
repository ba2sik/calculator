using System.Collections.Generic;

namespace Calculator.Core.Tokens
{
    public class NumberToken : MyToken
    {
        private string _value;

        public NumberToken(string str) : base(TokenTypes.Operator)
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

        public override void PerformAlgorithmStep(ref Stack<MyToken> operators, ref Queue<MyToken> output)
        {
            output.Enqueue(this);
        }
    }
}
