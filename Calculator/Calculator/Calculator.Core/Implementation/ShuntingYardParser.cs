using Calculator.Core.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculator.Core.Implementation
{
    public class ShuntingYardParser : IParser
    {
        private readonly Dictionary<char, int> _operatorsPrecedence;
        private readonly List<char> _leftAssociativeOperators;
        private readonly ITokenizer _tokenizer;

        public ShuntingYardParser(char[] operators)
        {
            _tokenizer = new CalculatorTokenizer(operators);
            _operatorsPrecedence = GetOperatrosPrecedence();
            _leftAssociativeOperators = new List<char> { '+', '-', '*', '/' };
        }

        private Dictionary<char, int> GetOperatrosPrecedence()
        {
            return new Dictionary<char, int>
            {
                { '+',1},
                { '-',1},
                { '*',2},
                { '/',2},
                { '^',3},
                { '(',4},
                { ')',4}
            };
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
            Queue<Token> output = new Queue<Token>();
            Stack<Token> operators = new Stack<Token>();

            foreach (var token in tokens)
            {
                HandleToken(token, ref operators, ref output);
            }

            while (operators.Any())
            {
                output.Enqueue(operators.Pop());
            }

            return output;
        }

        
        private void HandleToken(
            Token token,
            ref Stack<Token> operators,
            ref Queue<Token> output)
        {
            switch (token.type)
            {
                case TokenTypes.Number:
                    output.Enqueue(token);
                    break;

                case TokenTypes.Operator:
                    HandleOperatorToken(token, ref operators, ref output);
                    operators.Push(token);
                    break;

                case TokenTypes.LeftParenthesis:
                    operators.Push(token);
                    break;

                case TokenTypes.RightParenthesis:
                    while (operators.Peek().type != TokenTypes.LeftParenthesis)
                    {
                        output.Enqueue(operators.Pop());
                    }
                    operators.Pop();
                    break;

                default:
                    throw new ArgumentException($"Unknown token type: {token.type}");
            }
        }

        private void HandleOperatorToken(
            Token token,
            ref Stack<Token> operators,
            ref Queue<Token> output)
        {
            while (operators.Any() && operators.Peek().type == TokenTypes.Operator)
            {
                var op = operators.Peek();
                if ((IsTokenLeftAssociative(token) && (GetTokenPrecedence(token) <= GetTokenPrecedence(op))
                    || (!IsTokenLeftAssociative(token) && (GetTokenPrecedence(token) < GetTokenPrecedence(op)))))
                {
                    output.Enqueue(operators.Pop());
                }
                else
                {
                    break;
                }
            }
        }


        private int GetTokenPrecedence(Token t)
        {
            char op = char.Parse(t.value);
            return _operatorsPrecedence[op];
        }

        private bool IsTokenLeftAssociative(Token t)
        {
            char op = char.Parse(t.value);

            return _leftAssociativeOperators.Contains(op);
        }

        private double EvalPostfixExpression(Queue<Token> tokens)
        {
            Stack<double> operands = new Stack<double>();

            while (tokens.Any())
            {
                Token t = tokens.Dequeue();
                if (t.type == TokenTypes.Number)
                {
                    operands.Push(double.Parse(t.value));
                }
                else
                {
                    // It's stack, so the latter element is actually the first argument
                    double second = operands.Pop();
                    double first = operands.Pop();

                    double result = 0;

                    if (t.value == "+")
                    {
                        result = first + second;
                    }
                    else if (t.value == "-")
                    {
                        result = first - second;
                    }
                    else if (t.value == "*")
                    {
                        result = first * second;
                    }
                    else if (t.value == "/")
                    {
                        result = first / second;
                    }
                    else if (t.value == "^")
                    {
                        result = Math.Pow(first, second);
                    }

                    operands.Push(result);
                }
            }

            return operands.Pop();
        }
    }
}
