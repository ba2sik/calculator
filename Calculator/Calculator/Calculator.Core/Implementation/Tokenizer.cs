using Calculator.Core.Abstraction;
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
        public static List<Token> Tokenize(char[] arr, char[] operators)
        {
            // The first char in expression has special rules
            var firstToken = CreateFirstToken(arr[0]);
            var tokens = new List<Token>() { firstToken };

            // Skipping the first char
            foreach (var currentChar in arr.Skip(1))
            {
                HandleCharacter(currentChar, operators, tokens);
            }

            return tokens;
        }

        private static void HandleCharacter(char ch, char[] operators, List<Token> tokens)
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
            else if (ParserHelper.IsParenthesis(ch))
            {
                HandleParentheses(ch, tokens);
            }
            else
            {
                throw new ArgumentException($"Unknown character: {ch}\n");
            }
        }

        private static Token CreateFirstToken(char ch)
        {
            if (!ParserHelper.IsFirstCharacterValid(ch))
            {
                throw new ArgumentException($"The operator {ch} is in bad place\n");
            }

            return CreateToken(TokenTypes.ValidFirstCharacter, ch);
        }

        private static void HandleDigit(char digit, List<Token> tokens)
        {
            // AND in case its type is involving multiple types
            if ((tokens.Last().type & TokenTypes.Literal) == TokenTypes.Literal)
            {
                ConcatToLastToken(digit, tokens);
            }
            else
            {
                var token = CreateToken(getTokenType(digit), digit);
                tokens.Add(token);
            }
        }

        private static void HandleHyphen(List<Token> tokens)
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

        private static void HandleDot(List<Token> tokens)
        {
            if ((TokenTypes.Literal & getLastTokenType(tokens)) == 0)
            {
                throw new ArgumentException($"The sign . is in bad place\n");
            }

            ConcatToLastToken('.', tokens);
        }

        private static void HandleParentheses(char ch, List<Token> tokens)
        {
            var token = CreateToken(TokenTypes.Parenthesis, ch);
            tokens.Add(token);
        }

        private static void HandleOperator(char op, List<Token> tokens)
        {
            if ((TokenTypes.ValidTypeBeforeOperator & getLastTokenType(tokens)) == 0)
            {
                throw new ArgumentException($"You can't put two operators in a row (except '-')\n");
            }

            var token = CreateToken(getTokenType(op), op);
            tokens.Add(token);
        }

        private static Token CreateToken(TokenTypes type, char value)
        {
            return new CalculatorToken(type, value.ToString());
        }

        private static void ConcatToLastToken(char ch, List<Token> tokens)
        {
            tokens.Last().value += ch.ToString();
        }

        private static TokenTypes getLastTokenType(List<Token> tokens)
        {
            return tokens.Last().type;

        }
        private static TokenTypes getTokenType(char c)
        {
            return ParserHelper.IsDigit(c) ? TokenTypes.Literal : TokenTypes.Operator;
        }



    }
}
