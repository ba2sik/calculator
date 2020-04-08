using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Core.Implementation
{
    public static class Tokenizer
    {
        // TODO: switch to private after unit testing
        public static List<CalculatorToken> Tokenize(char[] arr, char[] operators)
        {
            // The first char in expression has special rules
            var firstToken = CreateFirstToken(arr[0]);
            var tokens = new List<CalculatorToken>() { firstToken };

            // Skipping the first char
            foreach (var currentChar in arr.Skip(1))
            {
                HandleCharacter(currentChar, operators, tokens);
            }

            return tokens;
        }

        private static void HandleCharacter(char ch, char[] operators, List<CalculatorToken> tokens)
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
            else if(ParserHelper.IsLeftParenthesis(ch) || 
                    ParserHelper.IsRightParenthesis(ch))
            {
                HandleParentheses(ch, tokens);
            }
            else
            {
                throw new ArgumentException($"Unknown character: {ch}\n");
            }
        }

        private static CalculatorToken CreateFirstToken(char ch)
        {
            // checking if there's another operator than hyphen
            if (getTokenType(ch) == TokenTypes.Operator && ch != '-')
            {
                throw new ArgumentException($"The operator {ch} is in bad place\n");
            }

            return CreateToken(TokenTypes.Literal, ch);
        }

        private static void HandleDigit(char digit, List<CalculatorToken> tokens)
        {
            if (tokens.Last().type == TokenTypes.Literal)
            {
                ConcatToLastToken(digit, tokens);
            }
            else
            {
                var token = CreateToken(getTokenType(digit), digit);
                tokens.Add(token);
            }
        }

        private static void HandleHyphen(List<CalculatorToken> tokens)
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

        private static void HandleDot(List<CalculatorToken> tokens)
        {
            if (getLastTokenType(tokens) != TokenTypes.Literal)
            {
                throw new ArgumentException($"The sign . is in bad place\n");
            }

            ConcatToLastToken('.', tokens);
        }

        private static void HandleParentheses(char ch, List<CalculatorToken> tokens)
        {
            var token = CreateToken(TokenTypes.Parenthesis, ch);
            tokens.Add(token);
        }

        private static void HandleOperator(char op, List<CalculatorToken> tokens)
        {
            if (getLastTokenType(tokens) == TokenTypes.Operator)
            {
                throw new ArgumentException($"You can't put two operators in a row (except '-')\n");
            }

            var token = CreateToken(getTokenType(op), op);
            tokens.Add(token);
        }

        private static CalculatorToken CreateToken(TokenTypes type, char value)
        {
            return new CalculatorToken(type, value.ToString());
        }

        private static void ConcatToLastToken(char ch, List<CalculatorToken> tokens)
        {
            tokens.Last().value += ch.ToString();
        }

        private static TokenTypes getLastTokenType(List<CalculatorToken> tokens)
        {
            return tokens.Last().type;

        }
        private static TokenTypes getTokenType(char c)
        {
            return ParserHelper.IsDigit(c) ? TokenTypes.Literal : TokenTypes.Operator;
        }



    }
}
