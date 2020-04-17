using System;
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

        public bool IsHyphen()
        {
            return _value.Length == 1 && !char.IsDigit(_value[0]);
        }

        public void ConcatCharacter(char c)
        {
            _value += c.ToString();
        }

        public override void PerformAlgorithmStep(ref Stack<Token> operators, ref Queue<Token> output)
        {
            output.Enqueue(this);
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;

            NumberToken other = obj as NumberToken;
            if ((Object)other == null)
                return false;

            // here you need to compare two objects
            // below is just example implementation

            return this._value == other._value;
        }
    }
}
