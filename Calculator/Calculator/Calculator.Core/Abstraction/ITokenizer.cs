using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Core.Abstraction
{
    public interface ITokenizer
    {
        List<Token> Tokenize(string expression);
    }
}
