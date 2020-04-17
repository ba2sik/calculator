using System.Collections.Generic;
using Calculator.Core;
using Calculator.Core.Exceptions;
using Calculator.Core.Helpers;
using Calculator.Core.Parser;
using Calculator.Core.Tokenizer;
using Calculator.Core.Tokens;
using Calculator.Core.Tokens.Factory;
using Calculator.Core.Tokens.Operators;
using Calculator.Core.Tokens.Parentheses;
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

        private List<OperatorToken> _operators = new List<OperatorToken>
        {
            new AdditionToken(),
            new SubtractionToken(),
            new MultiplicationToken(),
            new DivisionToken(),
            new PowerToken()
        };

        private ExpressionSymbols _symbols = new ExpressionSymbols();

        [TestMethod]
        public void NegativeExpression1()
        {
            // Arrange
            string str       = "-12*34";
            var    helper    = new ShuntingYardHelper(_operators, _symbols);
            var    tokenizer = new CalculatorTokenizer(helper);

            List<Token> expected = new List<Token>
            {
                new NumberToken("-12"),
                new MultiplicationToken(),
                new NumberToken("34")
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
            string str       = "-12--4";
            var    helper    = new ShuntingYardHelper(_operators, _symbols);
            var    tokenizer = new CalculatorTokenizer(helper);

            List<Token> expected = new List<Token>
            {
                new NumberToken("-12"),
                new SubtractionToken(),
                new NumberToken("-4")
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
            string str       = "-12.3+5.666";
            var    helper    = new ShuntingYardHelper(_operators, _symbols);
            var    tokenizer = new CalculatorTokenizer(helper);

            List<Token> expected = new List<Token>
            {
                new NumberToken("-12.3"),
                new AdditionToken(),
                new NumberToken("5.666")
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
            string str       = "1+(2*3)";
            var    helper    = new ShuntingYardHelper(_operators, _symbols);
            var    tokenizer = new CalculatorTokenizer(helper);

            List<Token> expected = new List<Token>
            {
                new NumberToken("1"),
                new AdditionToken(),
                new LeftParenthesisToken(),
                new NumberToken("2"),
                new MultiplicationToken(),
                new NumberToken("3"),
                new RightParenthesisToken()
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
            string             str    = "1+(2*3)";
            ShuntingYardParser parser = new ShuntingYardParser(_operators);
            var helper =
                new ShuntingYardHelper(_operators, _symbols);
            var tokenizer = new CalculatorTokenizer(helper);

            Queue<Token> expected = new Queue<Token>();
            expected.Enqueue(new NumberToken("1"));
            expected.Enqueue(new NumberToken("2"));
            expected.Enqueue(new NumberToken("3"));
            expected.Enqueue(new MultiplicationToken());
            expected.Enqueue(new AdditionToken());

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
            string             str    = "-12*(4+5)-2";
            ShuntingYardParser parser = new ShuntingYardParser(_operators);
            var helper =
                new ShuntingYardHelper(_operators, _symbols);
            var tokenizer = new CalculatorTokenizer(helper);

            Queue<Token> expected = new Queue<Token>();
            expected.Enqueue(new NumberToken("-12"));
            expected.Enqueue(new NumberToken("4"));
            expected.Enqueue(new NumberToken("5"));
            expected.Enqueue(new AdditionToken());
            expected.Enqueue(new MultiplicationToken());
            expected.Enqueue(new NumberToken("2"));
            expected.Enqueue(new SubtractionToken());

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
            string             str    = "12+3^2";
            ShuntingYardParser parser = new ShuntingYardParser(_operators);
            var helper =
                new ShuntingYardHelper(_operators, _symbols);
            var tokenizer = new CalculatorTokenizer(helper);

            Queue<Token> expected = new Queue<Token>();
            expected.Enqueue(new NumberToken("12"));
            expected.Enqueue(new NumberToken("3"));
            expected.Enqueue(new NumberToken("2"));
            expected.Enqueue(new PowerToken());
            expected.Enqueue(new AdditionToken());

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
            string             str      = "1+(2*3)";
            ShuntingYardParser parser   = new ShuntingYardParser(_operators);
            double             expected = 7;

            // Act
            // Assert
            var actual = parser.Parse(str);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void EvalPostfix2()
        {
            // Arrange
            string             str      = "-12.56+(9^2/3)";
            ShuntingYardParser parser   = new ShuntingYardParser(_operators);
            double             expected = 14.44;

            // Act
            // Assert
            var actual = parser.Parse(str);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void EvalPostfix3()
        {
            // Arrange
            string             str      = "-(2*3)^2+666";
            ShuntingYardParser parser   = new ShuntingYardParser(_operators);
            double             expected = 630;

            // Act
            // Assert
            var actual = parser.Parse(str);

            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void DotOnFirstCharacter_ShouldThrowTokenizationException()
        {
            // Arrange
            string str       = ".12*34";
            var    helper    = new ShuntingYardHelper(_operators, _symbols);
            var    tokenizer = new CalculatorTokenizer(helper);

            // Act
            // Assert
            Assert.ThrowsException<TokenizationException>(
                    () => tokenizer.Tokenize(str));
        }

        [TestMethod]
        public void TwoOperatorsInARow_ShouldThrowParsingException()
        {
            // Arrange
            string str       = "12**4";
            var    helper    = new ShuntingYardHelper(_operators, _symbols);
            var    tokenizer = new CalculatorTokenizer(helper);

            // Act
            // Assert
            Assert.ThrowsException<ParsingException>(() => tokenizer.Tokenize(str));
        }
    }
}