using System.Linq;

namespace Calculator.Core.Helper
{
    public abstract class Helper
    {
        public abstract char[] Operators { get; }

        public bool IsOperator(char c)
        {
            return Operators.Contains(c);
        }

        public bool IsDigit(char c)
        {
            return char.IsDigit(c);
        }

        public bool IsHyphen(char c)
        {
            return c == '-';
        }

        public string RemoveSpaces(string str)
        {
            return str.Replace(" ", "");
        }

        public bool IsParentheses(char c)
        {
            return IsLeftParenthesis(c) || IsRightParenthesis(c);
        }

        public virtual bool IsLeftParenthesis(char c)
        {
            return c == '(';
        }

        public virtual bool IsRightParenthesis(char c)
        {
            return c == ')';
        }
        public virtual bool IsDecimalSeperator(char c)
        {
            return c == '.';
        }

        public virtual bool IsHyphenMeansNegative(char priorCharacter)
        {
            // When hyphen is after number, it means subtraction. Else - negation
            return !IsDigit(priorCharacter);
        }

        public virtual bool IsFirstCharacterValid(char c)
        {
            return IsDigit(c) || IsHyphen(c) || IsParentheses(c);
        }
    }
}
