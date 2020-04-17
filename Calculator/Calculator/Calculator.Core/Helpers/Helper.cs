using Calculator.Core.Tokens;
using Calculator.Core.Tokens.Operators;
using Calculator.Core.Tokens.Parentheses;
using System.Collections.Generic;
using System.Linq;

namespace Calculator.Core.Helpers
{
    internal abstract class Helper
    {
        public readonly ExpressionSymbols symbols;

        public Dictionary<char, OperatorType> OperatorsDict { get; }
        protected List<OperatorToken> Operators { get; }

        protected Helper(List<OperatorToken> operators, ExpressionSymbols symbols)
        {
            Operators = operators;
            this.symbols = symbols;

            OperatorsDict = operators.ToDictionary(key => key.Sign,
                                                   value => value.OperatorType);
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
            return c == symbols.hyphen;
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
            return c == symbols.leftParenthesis;
        }

        public bool IsRightParenthesis(char c)
        {
            return c == symbols.rightParenthesis;
        }

        public bool IsDecimalSeparator(char c)
        {
            return c == symbols.decimalSeparator;
        }

        public virtual bool IsMinusUnary(ICollection<Token> tokens)
        {
            // Minus is unary if it's the first char, or after number / right parenthesis
            return !tokens.Any() || !(tokens.Last() is NumberToken ||
                                      tokens.Last() is RightParenthesisToken);
        }

        public virtual bool IsFirstTokenValid(Token t)
        {
            return t is NumberToken || t is LeftParenthesisToken ||
                   t is SubtractionToken;
        }

        public virtual bool IsLastTokenValid(Token t)
        {
            return t is NumberToken || t is RightParenthesisToken;
        }
    }
}