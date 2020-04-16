using Calculator.Core.Helper;
using Calculator.Core.Tokenizer;
using Calculator.Core.Tokens;
using Calculator.Core.Tokens.Operators;
using System.Collections.Generic;
using System.Linq;

namespace Calculator.Core.Parser
{
    public class ShuntingYardParser : IParser
    {
        private readonly ITokenizer _tokenizer;
        private readonly MyHelper _helper;

        public ShuntingYardParser()
        {
            _helper = new MyHelper();
            _tokenizer = new CalculatorTokenizer(_helper);
        }

        public double Parse(string expression)
        {
            List<Token> tokens = _tokenizer.Tokenize(expression);
            Queue<Token> postfixNotationTokens = GetPostfixNotation(tokens);
            double result = EvalPostfixExpression(postfixNotationTokens);

            return result;
        }

        public Queue<Token> GetPostfixNotation(IEnumerable<Token> tokens)
        {
            var operators = new Stack<Token>();
            var output = new Queue<Token>();

            foreach (var token in tokens)
            {
                token.PerformAlgorithmStep(ref operators, ref output);
            }

            _helper.PopStackToQueue(ref operators, ref output);

            return output;
        }

        private double EvalPostfixExpression(Queue<Token> tokens)
        {
            var operands = new Stack<NumberToken>();

            while (tokens.Any())
            {
                Token t = tokens.Dequeue();

                if (t.Type == TokenType.Number)
                {
                    operands.Push(t as NumberToken);
                }
                else if (operands.Count >= 2)
                {
                    // It's stack, so the latter element is actually the first argument
                    NumberToken second = operands.Pop();
                    NumberToken first = operands.Pop();

                    double result = (t as OperatorToken).Apply(first,second);
                    operands.Push(new NumberToken(result.ToString()));
                }
                //else
                //{
                //    throw new IllegalOperationException();
                //}
            }

            return operands.Pop().GetValue();
        }
    }
}
