using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ParseTests
{
    [TestClass]
    public class ParsingExpressionsTests
    {
        private TestContext testContextInstance;

        /// <summary>
        ///  Gets or sets the test context which provides
        ///  information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        /*
        [TestMethod]
        public void NegativeExpression1()
        {
            // Arrange
            string str = "-12*34";
            CalculatorTokenizer tokenizer = new CalculatorTokenizer(operators);

            List<CalculatorToken> expected = new List<CalculatorToken>
            {
                new CalculatorToken(TokenType.Number, "-12"),
                new CalculatorToken(TokenType.Operator, "*"),
                new CalculatorToken(TokenType.Number, "34")
            };

            // Act
            // Assert
            var actual = tokenizer.Tokenize(str);

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void NegativeExpression2()
        {
            // Arrange
            string str = "-12--4";

            CalculatorTokenizer tokenizer = new CalculatorTokenizer(operators);

            List<CalculatorToken> expected = new List<CalculatorToken>
            {
                new CalculatorToken(TokenType.Number, "-12"),
                new CalculatorToken(TokenType.Operator, "-"),
                new CalculatorToken(TokenType.Number, "-4")
            };

            // Act
            // Assert
            var actual = tokenizer.Tokenize(str);

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DotExpression1()
        {
            // Arrange
            string str = "-12.3+5.666";
            CalculatorTokenizer tokenizer = new CalculatorTokenizer(operators);

            List<CalculatorToken> expected = new List<CalculatorToken>
            {
                new CalculatorToken(TokenType.Number, "-12.3"),
                new CalculatorToken(TokenType.Operator, "+"),
                new CalculatorToken(TokenType.Number, "5.666")
            };

            // Act
            // Assert
            var actual = tokenizer.Tokenize(str);

            CollectionAssert.AreEqual(expected, actual);
        }

        // TODO
        [TestMethod]
        public void ParenthesisExpression1()
        {
            // Arrange
            string str = "1+(2*3)";
            CalculatorTokenizer tokenizer = new CalculatorTokenizer(operators);

            List<CalculatorToken> expected = new List<CalculatorToken>
            {
                new CalculatorToken(TokenType.Number, "1"),
                new CalculatorToken(TokenType.Operator, "+"),
                new CalculatorToken(TokenType.LeftParenthesis, "("),
                new CalculatorToken(TokenType.Number, "2"),
                new CalculatorToken(TokenType.Operator, "*"),
                new CalculatorToken(TokenType.Number, "3"),
                new CalculatorToken(TokenType.RightParenthesis, ")")
            };

            // Act
            // Assert
            var actual = tokenizer.Tokenize(str);

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Postfix1()
        {
            // Arrange
            string str = "1+(2*3)";
            char[] operators = { '+', '-', '*', '/', '^' };
            ShuntingYardParser parser = new ShuntingYardParser(operators);
            CalculatorTokenizer tokenizer = new CalculatorTokenizer(operators);

            Queue<CalculatorToken> expected = new Queue<CalculatorToken>();
            expected.Enqueue(new CalculatorToken(TokenType.Number, "1"));
            expected.Enqueue(new CalculatorToken(TokenType.Number, "2"));
            expected.Enqueue(new CalculatorToken(TokenType.Number, "3"));
            expected.Enqueue(new CalculatorToken(TokenType.Operator, "*"));
            expected.Enqueue(new CalculatorToken(TokenType.Operator, "+"));

            // Act
            // Assert
            var tokens = tokenizer.Tokenize(str);
            var actual = parser.GetPostfixNotation(tokens);

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Postfix2()
        {
            // Arrange
            string str = "-12*(4+5)-2";
            char[] operators = { '+', '-', '*', '/', '^' };
            ShuntingYardParser parser = new ShuntingYardParser(operators);
            CalculatorTokenizer tokenizer = new CalculatorTokenizer(operators);

            Queue<CalculatorToken> expected = new Queue<CalculatorToken>();
            expected.Enqueue(new CalculatorToken(TokenType.Number, "-12"));
            expected.Enqueue(new CalculatorToken(TokenType.Number, "4"));
            expected.Enqueue(new CalculatorToken(TokenType.Number, "5"));
            expected.Enqueue(new CalculatorToken(TokenType.Operator, "+"));
            expected.Enqueue(new CalculatorToken(TokenType.Number, "-2"));
            expected.Enqueue(new CalculatorToken(TokenType.Operator, "*"));

            // Act
            // Assert
            var tokens = tokenizer.Tokenize(str);
            var actual = parser.GetPostfixNotation(tokens);

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Postfix3()
        {
            // Arrange
            string str = "12+3^2";
            char[] operators = { '+', '-', '*', '/', '^' };
            ShuntingYardParser parser = new ShuntingYardParser(operators);
            CalculatorTokenizer tokenizer = new CalculatorTokenizer(operators);

            Queue<CalculatorToken> expected = new Queue<CalculatorToken>();
            expected.Enqueue(new CalculatorToken(TokenType.Number, "12"));
            expected.Enqueue(new CalculatorToken(TokenType.Number, "3"));
            expected.Enqueue(new CalculatorToken(TokenType.Number, "2"));
            expected.Enqueue(new CalculatorToken(TokenType.Operator, "^"));
            expected.Enqueue(new CalculatorToken(TokenType.Operator, "+"));

            // Act
            // Assert
            var tokens = tokenizer.Tokenize(str);
            var actual = parser.GetPostfixNotation(tokens);

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void EvalPostfix1()
        {
            // Arrange
            string str = "1+(2*3)";
            char[] operators = { '+', '-', '*', '/', '^' };
            ShuntingYardParser parser = new ShuntingYardParser(operators);
            double expected = 7;

            // Act
            // Assert
            var actual = parser.Parse(str);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void EvalPostfix2()
        {
            // Arrange
            string str = "-12.56+(9^2/3)";
            char[] operators = { '+', '-', '*', '/', '^' };
            ShuntingYardParser parser = new ShuntingYardParser(operators);
            double expected = 14.44;

            // Act
            // Assert
            var actual = parser.Parse(str);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DotOnFirstCharacter_ShouldThrowArgumentException()
        {
            // Arrange
            string str = ".12*34";

            CalculatorTokenizer tokenizer = new CalculatorTokenizer(operators);

            // Act
            // Assert
            Assert.ThrowsException<System.ArgumentException>(() =>
                    tokenizer.Tokenize(str));
        }

        [TestMethod]
        public void TwoOperatorsInARow_ShouldThrowArgumentException()
        {
            // Arrange
            string str = ".12**4";
            CalculatorTokenizer tokenizer = new CalculatorTokenizer(operators);

            // Act
            // Assert
            Assert.ThrowsException<System.ArgumentException>(() =>
                    tokenizer.Tokenize(str));
        }

        [TestMethod]
        public void OperatorOnFirstCharacter_ShouldThrowArgumentException()
        {
            // Arrange
            string str = "*12*34";
            CalculatorTokenizer tokenizer = new CalculatorTokenizer(operators);

            // Act
            // Assert
            Assert.ThrowsException<System.ArgumentException>(() =>
                    tokenizer.Tokenize(str));
        }

        [TestMethod]
        public void HyphenMeansNegative()
        {
            // Arrange
            char c = '*';
            bool expected = true;

            // Assert
            bool actual = ParserHelper.IsHyphenMeansNegative(c);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void HyphenMeansSubtraction()
        {
            // Arrange
            char c = '8';
            bool expected = false;

            // Assert
            bool actual = ParserHelper.IsHyphenMeansNegative(c);

            Assert.AreEqual(expected, actual);
        }*/
    }
}