using Calculator.Core.Tokens;
using System.Collections.Generic;

namespace Calculator.Core.Tokenizer
{
    internal interface PITokenizer
    {
        List<MyToken> Tokenize(string expression);
    }
}
