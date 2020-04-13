using Calculator.Core.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculator.Core.Implementation
{
    public class CalculatorParser : IParser
    {
        private ITokenizer _tokenizer;

        public CalculatorParser(char[] operators)
        {
            _tokenizer = new CalculatorTokenizer(operators);
        }

        public double Parse(string expression)
        {
            string cleanExpression = ParserHelper.RemoveSpaces(expression);
            char[] expArr = cleanExpression.ToCharArray();
            List<Token> tokens = _tokenizer.Tokenize(expArr);

            Console.WriteLine(cleanExpression);
            tokens.ForEach(t => Console.WriteLine(t.value));

            var expTree = CreateExpressionTree(tokens);
            double result = expTree.EvalTree();

            return result;
        }

        // TODO
        private ExpressionTokenTree CreateExpressionTree(List<Token> tokens)
        {
            var tree = new ExpressionTokenTree();

            return tree;
        }
    }
}
