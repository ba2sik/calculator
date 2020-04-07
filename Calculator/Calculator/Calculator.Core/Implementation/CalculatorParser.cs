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
            char currentChar = char.MinValue;
            char lastChar = char.MinValue;
            string currentCharStr = string.Empty;

            for (int i = 0; i < arr.Length; i++)
            {
                currentChar = arr[i];
                
                currentTokenType = getTokenType(currentChar);
                currentCharStr = currentChar.ToString();

                if (!tokens.Any())
                {
                    // checking if there's another operator than hyphen
                    if (currentTokenType == TokenTypes.Operator && currentChar != '-')
                    { 
                        throw new ArgumentException($"The operator {currentChar} is in bad place\n");
                    }
                    else
                    {
                        currentTokenType = TokenTypes.Literal;
                    }
                }
                else
                {
                    lastChar = arr[i - 1];

                    if (ParserHelper.IsDigit(currentChar))
                    {
                        // checking if the current digit is part of a larger number before it
                        if (lastToken.type == TokenTypes.Literal)
                        {
                            tokens.Last().value += currentCharStr;
                            continue;
                        }
                    }
                    else if (currentChar == '-')
                    {
                        if (ParserHelper.IsHyphenMeansNegative(lastChar))
                        {
                            currentTokenType = TokenTypes.Literal;
                        }
                    }
                    else if (currentChar == '.')
                    {
                        if (lastToken.type == TokenTypes.Literal && lastChar != '-')
                        {
                            tokens.Last().value += currentCharStr;
                            continue;
                        }
                        else
                        {
                            throw new ArgumentException($"The sign . is in bad place\n");
                        }
                    }
                    else if (!ParserHelper.IsOperator(currentChar, operators))
                    {
                        throw new ArgumentException($"Unknown character: {currentChar}\n");
                    }
                }

                lastToken = new CalculatorToken(currentTokenType, currentCharStr);
                tokens.Add(lastToken);
            }

            return tokens;
        }

        private static TokenTypes getTokenType(char c)
        {
            return ParserHelper.IsDigit(c) ? TokenTypes.Literal : TokenTypes.Operator;
        }

    }
}
