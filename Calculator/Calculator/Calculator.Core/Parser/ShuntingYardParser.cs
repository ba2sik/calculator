using Calculator.Core.Exceptions;
using Calculator.Core.Helpers;
using Calculator.Core.Tokenizer;
using Calculator.Core.Tokens;
using Calculator.Core.Tokens.Factory;
using Calculator.Core.Tokens.Operators;
using Calculator.Core.Tokens.Parentheses;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculator.Core.Parser
{
    public class ShuntingYardParser : IParser
    {
        private readonly ITokenizer _tokenizer;
        private readonly ShuntingYardHelper _helper;

        public ShuntingYardParser(List<OperatorToken> operators)
        {
            var symbols = new ExpressionSymbols();
            _helper = new ShuntingYardHelper(operators, symbols);
            _tokenizer = new CalculatorTokenizer(_helper);
        }

        public double Parse(string expression)
        {
            List<Token> tokens = _tokenizer.Tokenize(expression);
            ValidateTokens(tokens);
            Queue<Token> postfixNotationTokens = GetPostfixNotation(tokens);
            double result = EvalPostfixExpression(postfixNotationTokens);

            return result;
        }

        public Queue<Token> GetPostfixNotation(IEnumerable<Token> tokens) //CR: naming, Get at the start implies it is just a fetch operation, but it also performs logic
        {
            var operators = new Stack<Token>();
            var output    = new Queue<Token>();

            foreach (var token in tokens)
            {
                token.PerformAlgorithmStep(ref operators, ref output);
            }

            return PopRemainingOperators(operators, output);
        }

        public Queue<Token> PopRemainingOperators(Stack<Token> operators,
            Queue<Token> output)
        {
            while (operators.Any())
            {
                if (operators.Peek() is ParenthesisToken)
                {
                    throw new
                        ParsingException("Expression contains mismatched parentheses");
                }

                output.Enqueue(operators.Pop());
            }

            return output;
        }

        private double EvalPostfixExpression(Queue<Token> tokens)
        {
            var operands = new Stack<NumberToken>();
            double result;

            while (tokens.Any())
            {
                Token t = tokens.Dequeue();
                operands.Push(EvalToken(t, ref operands));
            }

            result = operands.Pop().GetValue();

            return result;
        }

        private NumberToken EvalToken(Token t, ref Stack<NumberToken> operands)
        {
            NumberToken tokenToPush;

            if (t is NumberToken token)
            {
                tokenToPush = token;
            }
            else
            {
                double result = 0;

                if (t is OperatorToken operatorToken && operands.Count >= 2)
                {
                    // It's stack, so the latter element is actually the first argument
                    NumberToken second = operands.Pop();
                    NumberToken first  = operands.Pop();

                    result = operatorToken.Apply(first, second);
                }
                // single hyphen means unary minus
                else if (t is SubtractionToken)
                {
                    result = -1 * operands.Pop().GetValue();
                }

                tokenToPush = new NumberToken(result.ToString());
            }

            return tokenToPush;
        }


//CR: since there are many kinds of validations you can perform (and they will get numerous if you'd try to support more complicated calculations),
// you should definitely consider extracting the validation process to its own separate class, have a validator.

//CR: design: having to validate your expression after doing part of the work has its downsides, 
//for example you have to do some kind of validation in the tokenzing process or else it would just break, and never reach the validation point, which is weird.
        private void ValidateTokens(List<Token> tokens) 
        {
            ValidateExpressionSides(tokens);
            ValidateOperatorsOrder(tokens);
            ConvertUnaryMinusesToSubtraction(tokens);
        }

        //CR: comment not needed
        // Check first and last tokens validity
        private void ValidateExpressionSides(IReadOnlyCollection<Token> tokens)
        {
            //CR: this is where the helper gets weird, here it is responsible for validation operations 
            // (which might not have a reason to be outside the validation class if you do end up going for it like i mentioned in the comment above)
            // but the helper also performs char and string operations for tokenizing in the tokenizer. 
            // it causes code to "spil" over to different parts and makes it so your helper class has no identity, it just "does things" for everyone. It may lead to it being bloated with random bits of code.
            if (!_helper.IsFirstTokenValid(tokens.First())) 
            {
                throw new
                    IllegalOperationException("Expression cannot start with an operator/right parenthesis");
            }

            if (!_helper.IsLastTokenValid(tokens.Last()))
            {
                throw new
                    IllegalOperationException("Expression cannot end with an operator/left parenthesis");
            }
        }

        // In case there is a hyphen and we have not been able to  
        // determine if it is part of a number (e.g: before parentheses)
        // we convert the token to a subtraction token.
        private void ConvertUnaryMinusesToSubtraction(List<Token> tokens)
        {
            for (int i = 0; i < tokens.Count; i++)
            {
                if (tokens[i] is NumberToken token && token.IsHyphen())
                {
                    tokens[i] = TokenFactory.Create(OperatorType.Subtraction);
                }
            }
        }

        // Search for consecutive operators
        private void ValidateOperatorsOrder(IReadOnlyList<Token> tokens)
        {
            for (int i = 0; i < tokens.Count - 1; i++)
            {
                if (tokens[i] is OperatorToken && tokens[i + 1] is OperatorToken)
                {
                    throw new IllegalOperationException(
                        "Expression cannot contain two consecutive operators (except minus)");
                }
            }
        }
    }
}