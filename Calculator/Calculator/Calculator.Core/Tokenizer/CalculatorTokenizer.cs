using Calculator.Core.Exceptions;
using Calculator.Core.Tokens;
using Calculator.Core.Tokens.Factory;
using Calculator.Core.Tokens.Operators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculator.Core.Tokenizer
{
    internal class CalculatorTokenizer : ITokenizer
    {
        private readonly Helper.Helper _helper;

        public CalculatorTokenizer(Helper.Helper helper)
        {
            _helper = helper;
        }

        public List<Token> Tokenize(string expression)
        {
            expression = Helper.Helper.RemoveSpaces(expression);
            var expressionCharacters = expression.ToCharArray();
            var i                    = 0;

            try
            {
                // The first character in the expression has special rules
                var firstToken = CreateFirstToken(expressionCharacters[0]);
                var tokens     = new List<Token> {firstToken};

                // Skipping the first character
                for (i = 1; i < expressionCharacters.Length; i++)
                {
                    HandleCharacter(expressionCharacters[i], tokens);
                }

                return tokens;
            }
            catch (InvalidOperationException e)
            {
                throw new IllegalOperationException(i, e.Message);
            }
            catch (KeyNotFoundException e)
            {
                throw new UnknownOperatorException(i, e.Message);
            }
            catch (ArgumentException e)
            {
                throw new ParsingException(i, e.Message);
            }
        }

        private Token CreateFirstToken(char c)
        {
            if (!_helper.IsFirstCharacterValid(c))
            {
                if (_helper.IsOperator(c))
                {
                    throw new InvalidOperationException($"The operator {c} is in illegal place.");
                }

                throw new KeyNotFoundException(c.ToString());
            }

            var type = _helper.IsParentheses(c)
                           ? GetParenthesisTokenType(c)
                           : TokenType.Number;

            return TokenFactory.Create(type, c);
        }

        private void HandleCharacter(char ch, List<Token> tokens)
        {
            if (_helper.IsHyphen(ch))
            {
                HandleHyphen(tokens);
            }
            else if (_helper.IsDecimalSeparator(ch))
            {
                HandleDecimalSeparator(tokens);
            }
            else if (Helper.Helper.IsDigit(ch))
            {
                HandleDigit(ch, tokens);
            }
            else if (_helper.IsOperator(ch))
            {
                HandleOperator(ch, tokens);
            }
            else if (_helper.IsParentheses(ch))
            {
                HandleParenthesis(ch, tokens);
            }
            else
            {
                throw new KeyNotFoundException(ch.ToString());
            }
        }

        private void HandleHyphen(ICollection<Token> tokens)
        {
            var token = _helper.IsHyphenMeansNegative(tokens.Last())
                            ? TokenFactory.Create(TokenType.Number, _helper.hyphen)
                            : TokenFactory.Create(OperatorType.Subtraction);

            tokens.Add(token);
        }

        private void HandleDecimalSeparator(IReadOnlyCollection<Token> tokens)
        {
            if (GetLastTokenType(tokens) != TokenType.Number)
            {
                throw new InvalidOperationException("Decimal separator is in bad place.");
            }

            ConcatToLastToken(_helper.decimalSeparator, tokens);
        }

        private void HandleDigit(char digit, ICollection<Token> tokens)
        {
            var lastToken = tokens.Last();

            if (lastToken is NumberToken)
            {
                ConcatToLastToken(digit, tokens);
            }
            else
            {
                tokens.Add(TokenFactory.Create(TokenType.Number, digit));
            }
        }

        private void HandleParenthesis(char ch, ICollection<Token> tokens)
        {
            var type = GetParenthesisTokenType(ch);
            tokens.Add(TokenFactory.Create(type));
        }

        private void HandleOperator(char op, List<Token> tokens)
        {
            var lastToken = tokens.Last();

            if (lastToken is OperatorToken)
            {
                throw new
                    InvalidOperationException("You can't put two operators in a row (except minus)");
            }

            OperatorType type = _helper.OperatorsDict[op];
            tokens.Add(TokenFactory.Create(type));
        }


        private static void ConcatToLastToken(char c, IEnumerable<Token> tokens)
        {
            (tokens.Last() as NumberToken).ConcatCharacter(c);
        }

        private static TokenType GetLastTokenType(IEnumerable<Token> tokens)
        {
            return tokens.Last().Type;
        }

        private TokenType GetParenthesisTokenType(char c)
        {
            return _helper.IsLeftParenthesis(c)
                       ? TokenType.LeftParenthesis
                       : TokenType.RightParenthesis;
        }
    }
}