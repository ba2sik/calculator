using Calculator.Core.Helper;
using Calculator.Core.Tokens;
using Calculator.Core.Tokens.Operators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculator.Core.Tokenizer
{
    internal class PCalculatorTokenizer : PITokenizer
    {
        private readonly PHelper _helper;

        public PCalculatorTokenizer(PHelper helper)
        {
            _helper = helper;
        }

        public List<MyToken> Tokenize(string expression)
        {
            expression = PHelper.RemoveSpaces(expression);
            char[] expArr = expression.ToCharArray();

            // The first char in expression has special rules
            var firstMyToken = CreateFirstToken(expArr[0]);
            var tokens = new List<MyToken>() { firstMyToken };

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

            var type = _helper.IsParentheses(c) ? TokenTypes.Parentheses
                                                : TokenTypes.Number;

            return TokenFactory.Create(type, c);
        }

        private void HandleCharacter(char ch, List<MyToken> tokens)
        {
            if (_helper.IsHyphen(ch))
            {
                HandleHyphen(tokens);
            }
            else if (_helper.IsDecimalSeperator(ch))
            {
                HandleDecimalSeperator(tokens);
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

        private void HandleHyphen(List<MyToken> tokens)
        {
            if (_helper.IsHyphenMeansNegative(tokens.Last()))
            {
                tokens.Add(TokenFactory.Create(TokenTypes.Number, _helper.hyphen));
            }
            else
            {
                tokens.Add(TokenFactory.Create(OperatorTypes.Subtraction));
            }
        }

        private void HandleDecimalSeperator(List<MyToken> tokens)
        {
            if (GetLastTokenType(tokens) != TokenTypes.Number)
            {
                throw new ArgumentException($"Your decimal seperator is in bad place\n");
            }

            ConcatToLastToken(_helper.decimalSeperator, tokens);
        }

        private void HandleDigit(char digit, List<MyToken> tokens)
        {
            var lastToken = tokens.Last();

            if (lastToken is NumberToken)
            {
                ConcatToLastToken(digit, tokens);
            }
            else
            {
                tokens.Add(TokenFactory.Create(TokenTypes.Number, digit));
            }
        }

        private void HandleParenthesis(char ch, List<MyToken> tokens)
        {
            TokenTypes type = GetParenthesisTokenType(ch);
            tokens.Add(TokenFactory.Create(type));
        }

        private void HandleOperator(char op, List<MyToken> tokens)
        {
            var lastToken = tokens.Last();

            if (lastToken is OperatorToken)
            {
                throw new ArgumentException($"You can't put two operators in a row (except minus)\n");
            }

            OperatorTypes type = _helper.OperatorsDict[op];
            tokens.Add(TokenFactory.Create(type));
        }


        private void ConcatToLastToken(char c, List<MyToken> tokens)
        {
            (tokens.Last() as NumberToken).ConcatCharacter(c);
        }

        private TokenTypes GetLastTokenType(List<MyToken> tokens)
        {
            return tokens.Last().type;

        }

        private TokenTypes GetParenthesisTokenType(char c)
        {
            if (_helper.IsLeftParenthesis(c))
            {
                return TokenTypes.LeftParenthesis;
            }

            return TokenTypes.RightParenthesis;
        }
    }
}
