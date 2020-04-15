using Calculator.Core.Abstraction;
using Calculator.Core.Helper;
using Calculator.Core.Tokenizer;
using Calculator.Core.Tokens;
using Calculator.Core.Tokens.Operators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculator.Core.Parser
{
    public class PShuntingYardParser : IParser
    {
        private readonly PITokenizer _tokenizer;
        private readonly MyHelper _helper;

        public PShuntingYardParser()
        {
            _helper = new MyHelper();
            _tokenizer = new PCalculatorTokenizer(_helper);
        }

        public double Parse(string expression)
        {
            List<MyToken> tokens = _tokenizer.Tokenize(expression);
            Queue<MyToken> postfixNotationTokens = GetPostfixNotation(tokens);
            double result = EvalPostfixExpression(postfixNotationTokens);

            return result;
        }

        public Queue<MyToken> GetPostfixNotation(IEnumerable<MyToken> tokens)
        {
            Stack<MyToken> operators = new Stack<MyToken>();
            Queue<MyToken> output = new Queue<MyToken>();

            foreach (var token in tokens)
            {
                token.PerformAlgorithmStep(ref operators, ref output);
            }

            _helper.PopStackToQueue(ref operators, ref output);

            return output;
        }

        private double EvalPostfixExpression(Queue<MyToken> tokens)
        {
            Stack<NumberToken> operands = new Stack<NumberToken>();

            while (tokens.Any())
            {
                MyToken t = tokens.Dequeue();

                if (t.type == TokenTypes.Number)
                {
                    operands.Push(t as NumberToken);
                }
                else
                {
                    // It's stack, so the latter element is actually the first argument
                    NumberToken second = operands.Pop();
                    NumberToken first = operands.Pop();

                    double result = (t as OperatorToken).Apply(first,second);
                    operands.Push(new NumberToken(result.ToString()));
                }
            }

            return operands.Pop().GetValue();
        }
    }
}
