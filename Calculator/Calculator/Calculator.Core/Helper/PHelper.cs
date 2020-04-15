using Calculator.Core.Tokens;
using Calculator.Core.Tokens.Operators;
using System.Collections.Generic;

namespace Calculator.Core.Helper
{
    internal abstract class PHelper
    {
        public readonly char hyphen;
        public readonly char leftParenthesis;
        public readonly char rightParenthesis;
        public readonly char decimalSeparator;

        public abstract Dictionary<char, OperatorType> OperatorsDict { get; }
        protected abstract List<OperatorToken> Operators { get; }

        protected PHelper(
            char hyphen = '-',
            char leftParenthesis = '(',
            char rightParenthesis = ')',
            char decimalSeparator = '.')
        {
            this.hyphen = hyphen;
            this.leftParenthesis = leftParenthesis;
            this.rightParenthesis = rightParenthesis;
            this.decimalSeparator = decimalSeparator;
        }

        public bool IsOperator(char c)
        {
            return Operators.Exists(op => op.Sign == c);
        }

        public static bool IsDigit(char c)
        {
            return char.IsDigit(c);
        }

        public bool IsHyphen(char c)
        {
            return c == hyphen;
        }

        public static string RemoveSpaces(string str)
        {
            return str.Replace(" ", "");
        }

        public bool IsParentheses(char c)
        {
            return IsLeftParenthesis(c) || IsRightParenthesis(c);
        }

        public bool IsLeftParenthesis(char c)
        {
            return c == leftParenthesis;
        }

        public bool IsRightParenthesis(char c)
        {
            return c == rightParenthesis;
        }

        public bool IsDecimalSeparator(char c)
        {
            return c == decimalSeparator;
        }

        public virtual bool IsHyphenMeansNegative(MyToken previousToken)
        {
            // When hyphen is after number, it means subtraction. Else - negation
            return !(previousToken is NumberToken);
        }

        public virtual bool IsFirstCharacterValid(char c)
        {
            return IsDigit(c) || IsHyphen(c) || IsParentheses(c);
        }
    }
}
