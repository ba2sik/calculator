using Calculator.Core.Abstraction;
using Calculator.Core.Tokens;
using Calculator.Core.Tokens.Operators;
using System.Collections.Generic;
using System.Linq;

namespace Calculator.Core.Helper
{
    internal class MyHelper : PHelper
    {
        public override Dictionary<char, OperatorTypes> OperatorsDict { get; }
        protected override List<OperatorToken> Operators { get; }

        public MyHelper() : base()
        {
            Operators = new List<OperatorToken>
            {
                new AdditionToken(),
                new SubtractionToken(),
                new MultiplicationToken(),
                new DivisionToken(),
                new PowerToken(),
            };
            OperatorsDict = Operators.ToDictionary(
                key => key.sign, 
                value => value.operatorType);
        }

        public void PopStackToQueue(ref Stack<MyToken> s, ref Queue<MyToken> q)
        {
            while (s.Any())
            {
                q.Enqueue(s.Pop());
            }
        }
        public bool IsThereOperators(Stack<Token> s)
        {
            return s.Any() && s.Peek().type == TokenTypes.Operator;
        }

        public static bool ShouldPopOperator(OperatorToken a, OperatorToken b)
        {
            return a.isLeftAssociative ? a.precedence <= b.precedence
                                       : a.precedence < b.precedence;
        }
    }
}
