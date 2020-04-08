namespace Calculator.Core
{
    public enum TokenTypes
    {
        Literal = 1,
        Operator = 2,
        Parenthesis = 4,
        // If the character before the operator is not  
        // literal or parenthesis, there's an parsing error
        ValidTypeBeforeOperator = Literal | Parenthesis,
        ValidFirstCharacter = ValidTypeBeforeOperator
    }
}
