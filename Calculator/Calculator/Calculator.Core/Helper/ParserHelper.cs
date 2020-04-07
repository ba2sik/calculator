using System.Linq;

namespace Calculator.Core.Helper
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

        public static bool isLeftParenthesis(char ch)
        {
            return ch == '(';
        }

        public static bool isRightParenthesis(char ch)
        {
            return ch == ')';
        }
    }
}
