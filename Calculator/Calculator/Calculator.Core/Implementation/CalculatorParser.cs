using Calculator.Core.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculator.Core.Implementation
{
    public class CalculatorParser : IParser
    {
        private readonly char[] _operators;

        public CalculatorParser(char[] operators)
        {
            _operators = operators;
        }

        public double Parse(string exp)
        {
            exp = ParserHelper.RemoveSpaces(exp);
            char[] expArr = exp.ToCharArray();
            List<CalculatorToken> tokens = Tokenize(expArr,_operators);
            
            System.Console.WriteLine(exp);

            return 6.666;
        }
      
        public static List<CalculatorToken> Tokenize(char[] arr,char[] operators)
        {
            var tokens = new List<CalculatorToken>();
            TokenTypes currentTokenType = TokenTypes.Literal;
            CalculatorToken lastToken = null;
            char lastChar = char.MinValue;
            string currentCharStr = string.Empty;

            foreach (char currentChar in arr)
            {
                currentTokenType = getTokenType(currentChar);
                currentCharStr = currentChar.ToString();

                if (!tokens.Any())
                {
                    // checking if there's another operator than hyphen
                    if (currentTokenType == TokenTypes.Operator && currentChar != '-')
                    { 
                        throw new ArgumentException($"The operator {currentChar} is in bad place\n");
                    }

                    lastToken = new CalculatorToken(TokenTypes.Literal, currentCharStr);
                    tokens.Add(lastToken);
                }
                else
                {
                    if (ParserHelper.IsDigit(currentChar))
                    {
                        // checking if the current digit is part of a larger number before it
                        if (lastToken.type == TokenTypes.Literal)
                        {
                            tokens.Last().value += currentCharStr;
                        }
                        else
                        {
                            lastToken = new CalculatorToken(TokenTypes.Literal, currentCharStr);
                            tokens.Add(lastToken);
                        }
                    }
                    else if (currentChar == '-')
                    {
                        if (ParserHelper.IsHyphenMeansNegative(lastChar))
                        {
                            lastToken = new CalculatorToken(TokenTypes.Literal, currentCharStr);
                        }
                        else
                        {
                            lastToken = new CalculatorToken(TokenTypes.Operator, currentCharStr);
                        }

                        tokens.Add(lastToken);
                    }
                    else if (ParserHelper.IsOperator(currentChar, operators))
                    {
                        lastToken = new CalculatorToken(TokenTypes.Operator, currentCharStr);
                        tokens.Add(lastToken);
                    }
                    else
                    {
                        throw new ArgumentException($"Unknown character: {currentChar}\n");
                    }
                }

                lastToken = tokens.Last();
                lastChar = currentChar;
            }

            return tokens;
        }

        private static TokenTypes getTokenType(char c)
        {
            return ParserHelper.IsDigit(c) ? TokenTypes.Literal : TokenTypes.Operator;
        }

    }
}
