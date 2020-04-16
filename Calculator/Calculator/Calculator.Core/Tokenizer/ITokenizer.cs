using Calculator.Core.Tokens;
using System.Collections.Generic;

namespace Calculator.Core.Tokenizer
{
    internal interface ITokenizer
    {
        List<Token> Tokenize(string expression);
    }
}
