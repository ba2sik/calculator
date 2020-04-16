using Calculator.Core.Tokens;
using Calculator.Core.Tokens.Operators;
using System.Collections.Generic;
using System.Linq;

namespace Calculator.Core.Helper
{
    internal class MyHelper : Helper
    {
        public override Dictionary<char, OperatorType> OperatorsDict { get; }
        protected override List<OperatorToken> Operators { get; }

        public MyHelper()
        {
            Operators = new List<OperatorToken>
            {
                new AdditionToken(),
                new SubtractionToken(),
                new MultiplicationToken(),
                new DivisionToken(),
                new PowerToken()
            };

            OperatorsDict = Operators.ToDictionary(
                key => key.Sign,
                value => value.OperatorType);
        }

        public void PopStackToQueue(ref Stack<Token> s, ref Queue<Token> q)
        {
            while (s.Any())
            {
                q.Enqueue(s.Pop());
            }
        }

        public static bool OperatorAtTop(Stack<Token> s)
        {
            return s.Any() && s.Peek() is OperatorToken;
        }

        public static bool ShouldPopOperator(OperatorToken a, OperatorToken b)
        {
            return a.IsLeftAssociative ? a.Precedence <= b.Precedence
                                       : a.Precedence < b.Precedence;
        }
    }
}
