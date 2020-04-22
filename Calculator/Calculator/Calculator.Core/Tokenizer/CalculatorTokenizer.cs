using Calculator.Core.Exceptions;
using Calculator.Core.Helpers;
using Calculator.Core.Tokens;
using Calculator.Core.Tokens.Factory;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculator.Core.Tokenizer
{
    internal class CalculatorTokenizer : ITokenizer
    {
        private readonly Helper _helper;

        public CalculatorTokenizer(Helper helper)
        {
            _helper = helper;
        }

        public List<Token> Tokenize(string expression)
        {
            expression = Helper.RemoveSpaces(expression);
            var expressionCharacters = expression.ToCharArray();
            var tokens               = new List<Token>(); //CR: formatting?
            int i                    = 0;

            try
            {
                for (i = 0; i < expressionCharacters.Length; i++)
                {
                    HandleCharacter(expressionCharacters[i], tokens);
                }

                return tokens;
            }
            catch (InvalidOperationException e)
            {
                throw new TokenizationException(i, e.Message);
            }
            catch (KeyNotFoundException e)
            {
                throw new UnknownOperatorException(i, e.Message);
            }
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
            else if (Helper.IsDigit(ch))
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
            var token = _helper.IsMinusUnary(tokens)
                            ? TokenFactory.Create(TokenType.Number, _helper.symbols.hyphen)
                            : TokenFactory.Create(OperatorType.Subtraction);

            tokens.Add(token);
        }

        private void HandleDecimalSeparator(IReadOnlyCollection<Token> tokens)
        {
            if (!tokens.Any() || !(tokens.Last() is NumberToken)) //CR: this is validation, see the comments in the parser
            {
                throw new InvalidOperationException(
                        "Decimal separator is in bad place.");
            }

            ConcatToLastToken(_helper.symbols.decimalSeparator, tokens);
        }

        private void HandleDigit(char digit, ICollection<Token> tokens)
        {
            if (tokens.Any() && tokens.Last() is NumberToken)
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

        private void HandleOperator(char op, ICollection<Token> tokens)
        {
            OperatorType type = _helper.OperatorsDict[op];
            tokens.Add(TokenFactory.Create(type));
        }


        private static void ConcatToLastToken(char c, IEnumerable<Token> tokens)
        {
            (tokens.Last() as NumberToken).ConcatCharacter(c);
        }

        private TokenType GetParenthesisTokenType(char c)
        {
            return _helper.IsLeftParenthesis(c)
                       ? TokenType.LeftParenthesis
                       : TokenType.RightParenthesis;
        }
    }
}