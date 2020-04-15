using System.Collections.Generic;

namespace Calculator.Core.Abstraction
{
    public interface ITokenizer
    {
        List<Token> Tokenize(string expression);
    }
}
