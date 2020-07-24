namespace Calculator.Core.Helpers
{
    public class ExpressionSymbols
    {
        public readonly char hyphen;
        public readonly char leftParenthesis;
        public readonly char rightParenthesis;
        public readonly char decimalSeparator;

        public ExpressionSymbols(
            char hyphen = '-',
            char leftParenthesis = '(',
            char rightParenthesis = ')',
            char decimalSeparator = '.')
        {
            this.hyphen = hyphen;
            this.leftParenthesis = leftParenthesis;
            this.rightParenthesis = rightParenthesis;
            this.decimalSeparator = decimalSeparator;
        }
    }
}
