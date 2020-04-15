using Calculator.Core.Helper;
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
        private readonly PHelper _helper;

        public CalculatorTokenizer(PHelper helper)
        {
            _helper = helper;
        }

        public List<MyToken> Tokenize(string expression)
        {
            expression = PHelper.RemoveSpaces(expression);
            var expArr = expression.ToCharArray();

            // The first char in expression has special rules
            var firstToken = CreateFirstToken(expArr[0]);
            var tokens = new List<MyToken> { firstToken };

            // Skipping the first char
            foreach (var currentChar in expArr.Skip(1))
            {
                HandleCharacter(currentChar, tokens);
            }

            return tokens;
        }

        private MyToken CreateFirstToken(char c)
        {
            if (!_helper.IsFirstCharacterValid(c))
            {
                throw new ArgumentException($"The operator {c} is in bad place\n");
            }

            var type = _helper.IsParentheses(c) ? GetParenthesisTokenType(c)
                                                : TokenType.Number;

            return TokenFactory.Create(type, c);
        }

        private void HandleCharacter(char ch, List<MyToken> tokens)
        {
            if (_helper.IsHyphen(ch))
            {
                HandleHyphen(tokens);
            }
            else if (_helper.IsDecimalSeparator(ch))
            {
                HandleDecimalSeparator(tokens);
            }
            else if (PHelper.IsDigit(ch))
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
                throw new ArgumentException($"Unknown character: {ch}\n");
            }
        }

        private void HandleHyphen(ICollection<MyToken> tokens)
        {
            var token = _helper.IsHyphenMeansNegative(tokens.Last())
                            ? TokenFactory.Create(TokenType.Number, _helper.hyphen)
                            : TokenFactory.Create(OperatorTypes.Subtraction);

            tokens.Add(token);
        }

        private void HandleDecimalSeparator(IReadOnlyCollection<MyToken> tokens)
        {
            if (GetLastTokenType(tokens) != TokenType.Number)
            {
                throw new ArgumentException("Your decimal separator is in bad place\n");
            }

            ConcatToLastToken(_helper.decimalSeparator, tokens);
        }

        private void HandleDigit(char digit, ICollection<MyToken> tokens)
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

        private void HandleParenthesis(char ch, ICollection<MyToken> tokens)
        {
            var type = GetParenthesisTokenType(ch);
            tokens.Add(TokenFactory.Create(type));
        }

        private void HandleOperator(char op, List<MyToken> tokens)
        {
            if (tokens == null) throw new ArgumentNullException(nameof(tokens));
            var lastToken = tokens.Last();

            if (lastToken is OperatorToken)
            {
                throw new ArgumentException("You can't put two operators in a row (except minus)\n");
            }

            OperatorTypes type = _helper.OperatorsDict[op];
            tokens.Add(TokenFactory.Create(type));
        }


        private static void ConcatToLastToken(char c, IEnumerable<MyToken> tokens)
        {
            (tokens.Last() as NumberToken).ConcatCharacter(c);
        }

        private static TokenType GetLastTokenType(IEnumerable<MyToken> tokens)
        {
            return tokens.Last().type;

        }

        private TokenType GetParenthesisTokenType(char c)
        {
            return _helper.IsLeftParenthesis(c)
                       ? TokenType.LeftParenthesis
                       : TokenType.RightParenthesis;
        }
    }
}
