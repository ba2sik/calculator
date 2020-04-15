using System.Collections.Generic;

namespace Calculator.Core.Helper
{
    public class MyHelper : Helper
    {
        public override char[] Operators { get; }

        public override char[] LeftAssociativeOperators { get; }

        public override Dictionary<char, int> OperatorsPrecedence { get; }

        public MyHelper() : base()
        {
            Operators = new char[] { '+', '-', '*', '/', '^' };
            LeftAssociativeOperators = new char[] { '+', '-', '*', '/' };
            OperatorsPrecedence = new Dictionary<char, int>
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
    }
}
