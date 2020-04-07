using Calculator.Core.Abstraction;

namespace Calculator.Core.Implementation
{
    public class TokenTreeNode
    {
        public TokenTreeNode leftNode;
        public TokenTreeNode rightNode;
        public Token data;

        public TokenTreeNode(Token data)
        {
            this.data = data;
            this.leftNode = null;
            this.rightNode = null;
        }
    }
}
