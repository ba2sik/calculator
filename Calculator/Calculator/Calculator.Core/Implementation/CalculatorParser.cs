using Calculator.Core.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculator.Core.Implementation
{
    public class CalculatorParser : IParser
    {
        private readonly ITokenizer _tokenizer;

        public CalculatorParser(char[] operators)
        {
            _tokenizer = new CalculatorTokenizer(operators);
        }

        public double Parse(string expression)
        {
            List<Token> tokens = _tokenizer.Tokenize(expression);
            Stack<Token> postfixNotationTokens = GetPostfixNotation(tokens);
            double result = EvalPostfixExpression(postfixNotationTokens);

            Console.WriteLine(expression);
            tokens.ForEach(t => Console.WriteLine(t.value));


            return result;
        }

        private Stack<Token> GetPostfixNotation(List<Token> tokens)
        {
            return new Stack<Token>(tokens);
        }
        
        private double EvalPostfixExpression(Stack<Token> tokens)
        {

            return 6.666;
        }
    }
}
