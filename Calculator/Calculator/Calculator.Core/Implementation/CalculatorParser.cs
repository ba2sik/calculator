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
            List<CalculatorToken> tokens = Tokenizer.Tokenize(expArr, _operators);

            Console.WriteLine(expArr);
            tokens.ForEach(t => Console.WriteLine(t.value));

            var expTree = CreateExpressionTree(tokens);
            double result = expTree.EvalTree();

            return result;
        }

        // TODO
        private ExpressionTokenTree CreateExpressionTree(List<CalculatorToken> tokens)
        {
            var tree = new ExpressionTokenTree();

            return tree;
        }
    }
}
