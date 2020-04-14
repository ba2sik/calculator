using Calculator.Core.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculator.Core.Implementation
{
    public class CalculatorTokenizer : ITokenizer
    {
        private char[] _operators = null;

        public CalculatorTokenizer(char[] operators)
        {
            _operators = operators;
        }

        public List<Token> Tokenize(string expression)
        {
            string cleanExpression = ParserHelper.RemoveSpaces(expression);
            char[] expArr = cleanExpression.ToCharArray();

            // The first char in expression has special rules
            var firstToken = CreateFirstToken(expArr[0]);
            var tokens = new List<Token>() { firstToken };

            // Skipping the first char
            foreach (var currentChar in expArr.Skip(1))
            {
                HandleCharacter(currentChar, _operators, tokens);
            }

            return tokens;
        }

        private void HandleCharacter(char ch, char[] operators, List<Token> tokens)
        {
            if (ParserHelper.IsHyphen(ch))
            {
                HandleHyphen(tokens);
            }
            else if (ParserHelper.IsDot(ch))
            {
                HandleDot(tokens);
            }
            else if (ParserHelper.IsDigit(ch))
            {
                HandleDigit(ch, tokens);
            }
            else if (ParserHelper.IsOperator(ch, operators))
            {
                HandleOperator(ch, tokens);
            }
            else if (ParserHelper.IsParentheses(ch))
            {
                HandleParenthesis(ch, tokens);
            }
            else
            {
                throw new ArgumentException($"Unknown character: {ch}\n");
            }
        }

        private Token CreateFirstToken(char ch)
        {
            if (!ParserHelper.IsFirstCharacterValid(ch))
            {
                throw new ArgumentException($"The operator {ch} is in bad place\n");
            }

            var type = ParserHelper.IsParentheses(ch) ? TokenTypes.Parentheses
                                                      : TokenTypes.Literal;
            return CreateToken(type, ch);
        }

        private void HandleDigit(char digit, List<Token> tokens)
        {
            // AND in case its type is involving multiple types
            if ((tokens.Last().type & TokenTypes.Literal) == TokenTypes.Literal)
            {
                ConcatToLastToken(digit, tokens);
            }
            else
            {
                var token = CreateToken(GetTokenType(digit), digit);
                tokens.Add(token);
            }
        }

        private void HandleHyphen(List<Token> tokens)
        {
            // get last character at the last token's value
            char lastChar = tokens.Last().value.Last();
            TokenTypes type = TokenTypes.Operator;

            if (ParserHelper.IsHyphenMeansNegative(lastChar))
            {
                type = TokenTypes.Literal;
            }

            var token = CreateToken(type, '-');
            tokens.Add(token);
        }

        private void HandleDot(List<Token> tokens)
        {
            if ((TokenTypes.Literal & GetLastTokenType(tokens)) == 0)
            {
                throw new ArgumentException($"The sign . is in bad place\n");
            }

            ConcatToLastToken('.', tokens);
        }

        private void HandleParenthesis(char ch, List<Token> tokens)
        {
            TokenTypes type = GetParenthesisTokenType(ch);
            var token = CreateToken(type, ch);
            tokens.Add(token);
        }

        private void HandleOperator(char op, List<Token> tokens)
        {
            if ((TokenTypes.ValidTypeBeforeOperator & GetLastTokenType(tokens)) == 0)
            {
                throw new ArgumentException($"You can't put two operators in a row (except '-')\n");
            }

            var token = CreateToken(GetTokenType(op), op);
            tokens.Add(token);
        }

        private Token CreateToken(TokenTypes type, char value)
        {
            return new CalculatorToken(type, value.ToString());
        }

        private void ConcatToLastToken(char ch, List<Token> tokens)
        {
            tokens.Last().value += ch.ToString();
        }

        private TokenTypes GetLastTokenType(List<Token> tokens)
        {
            return tokens.Last().type;

        }
        private TokenTypes GetTokenType(char c)
        {
            if (ParserHelper.IsDigit(c))
            {
                return TokenTypes.Literal;
            }
            if (ParserHelper.IsParentheses(c))
            {
                return TokenTypes.Parentheses;
            }

            return TokenTypes.Operator;
        }

        private TokenTypes GetParenthesisTokenType(char c)
        {
            if (ParserHelper.IsLeftParenthesis(c))
            {
                return TokenTypes.LeftParenthesis;
            }

            return TokenTypes.RightParenthesis;
        }
    }
}
