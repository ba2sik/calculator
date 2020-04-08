using System.Linq;

namespace Calculator.Core
{
    public static class ParserHelper
    {
        public static bool IsDigit(char ch)
        {
            return ch >= '0' && ch <= '9';
        }

        public static bool IsOperator(char ch, char[] operators)
        {
            return operators.Contains(ch);
        }

        public static bool IsLeftParenthesis(char ch)
        {
            return ch == '(';
        }

        public static bool IsRightParenthesis(char ch)
        {
            return ch == ')';
        }

        public static bool IsHyphen(char ch)
        {
            return ch == '-';
        }

        public static bool IsDot(char ch)
        {
            return ch == '.';
        }

        public static string RemoveSpaces(string str)
        {
            return str.Replace(" ", "");
        }

        public static bool IsHyphenMeansNegative(char priorCharacter)
        {
            return !IsDigit(priorCharacter);
        }
    }
}
