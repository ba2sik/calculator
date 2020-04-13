using Microsoft.VisualStudio.TestTools.UnitTesting;
using Calculator.Core.Implementation;
using System.Collections.Generic;
using Calculator.Core;
using System.Linq;

namespace ParseTests
{
    [TestClass]
    public class ParsingExpressionsTests
    {
        public static readonly char[] operators = { '+', '-', '*', '/', '^' };
        
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

        [TestMethod]
        public void NegativeExpression1()
        {
            // Arrange
            char[] arr = { '-', '1', '2', '*', '3', '4' };
            CalculatorTokenizer tokenizer = new CalculatorTokenizer(operators);


        List<CalculatorToken> expected = new List<CalculatorToken>();
            expected.Add(new CalculatorToken(TokenTypes.ValidFirstCharacter, "-12"));
            expected.Add(new CalculatorToken(TokenTypes.Operator, "*"));
            expected.Add(new CalculatorToken(TokenTypes.Literal, "34"));

            // Act
            // Assert
            var actual = tokenizer.Tokenize(arr);

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void NegativeExpression2()
        {
            // Arrange
            char[] arr = { '-', '1', '2', '-', '-', '4' };
            CalculatorTokenizer tokenizer = new CalculatorTokenizer(operators);

            List<CalculatorToken> expected = new List<CalculatorToken>();
            expected.Add(new CalculatorToken(TokenTypes.ValidFirstCharacter, "-12"));
            expected.Add(new CalculatorToken(TokenTypes.Operator, "-"));
            expected.Add(new CalculatorToken(TokenTypes.Literal, "-4"));

            // Act
            // Assert
            var actual = tokenizer.Tokenize(arr);

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DotExpression1()
        {
            // Arrange
            char[] arr = { '-', '1', '2', '.', '3', '+', '5', '.', '6', '6', '6', };
            CalculatorTokenizer tokenizer = new CalculatorTokenizer(operators);

            List<CalculatorToken> expected = new List<CalculatorToken>();
            expected.Add(new CalculatorToken(TokenTypes.ValidFirstCharacter, "-12.3"));
            expected.Add(new CalculatorToken(TokenTypes.Operator, "+"));
            expected.Add(new CalculatorToken(TokenTypes.Literal, "5.666"));

            // Act
            // Assert
            var actual = tokenizer.Tokenize(arr);

            CollectionAssert.AreEqual(expected, actual);
        }

        // TODO
        [TestMethod]
        public void ParenthesisExpression1()
        {
            // Arrange
            char[] arr = { '1', '+', '(', '2', '*', '3', ')'};
            CalculatorTokenizer tokenizer = new CalculatorTokenizer(operators);

            List<CalculatorToken> expected = new List<CalculatorToken>();
            expected.Add(new CalculatorToken(TokenTypes.ValidFirstCharacter, "1"));
            expected.Add(new CalculatorToken(TokenTypes.Operator, "+"));
            expected.Add(new CalculatorToken(TokenTypes.Parenthesis, "("));
            expected.Add(new CalculatorToken(TokenTypes.Literal, "2"));
            expected.Add(new CalculatorToken(TokenTypes.Operator, "*"));
            expected.Add(new CalculatorToken(TokenTypes.Literal, "3"));
            expected.Add(new CalculatorToken(TokenTypes.Parenthesis, ")"));

            // Act
            // Assert
            var actual = tokenizer.Tokenize(arr);

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DotOnFirstCharacter_ShouldThrowArgumentException()
        {
            // Arrange
            char[] arr = { '.', '1', '2', '*', '3', '4' };
            CalculatorTokenizer tokenizer = new CalculatorTokenizer(operators);

            // Act
            // Assert
            Assert.ThrowsException<System.ArgumentException>(() =>
                    tokenizer.Tokenize(arr));

        }

        [TestMethod]
        public void TwoOperatorsInARow_ShouldThrowArgumentException()
        {
            // Arrange
            char[] arr = { '.', '1', '2', '*', '*', '4' };
            CalculatorTokenizer tokenizer = new CalculatorTokenizer(operators);

            // Act
            // Assert
            Assert.ThrowsException<System.ArgumentException>(() =>
                    tokenizer.Tokenize(arr));

        }

        [TestMethod]
        public void OperatorOnFirstCharacter_ShouldThrowArgumentException()
        {
            // Arrange
            char[] arr = { '*', '1', '2', '*', '3', '4' };
            CalculatorTokenizer tokenizer = new CalculatorTokenizer(operators);

            // Act
            // Assert
            Assert.ThrowsException<System.ArgumentException>(() =>
                    tokenizer.Tokenize(arr));

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

        }
    }
}