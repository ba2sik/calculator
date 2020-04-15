namespace Calculator.Core
{
    public enum TokenTypes : uint
    {
        Number = 1,
        Operator = 2,
        LeftParenthesis = 4,
        RightParenthesis = 8,
        Parentheses = LeftParenthesis | RightParenthesis,
        // If the character before the operator is not  
        // literal or parenthesis, there's an parsing error
        ValidTypeBeforeOperator = Number | Parentheses,
        ValidFirstCharacter = ValidTypeBeforeOperator
    }
}
