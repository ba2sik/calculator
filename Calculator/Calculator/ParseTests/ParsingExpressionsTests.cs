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

            List<CalculatorToken> expected = new List<CalculatorToken>();
            expected.Add(new CalculatorToken(TokenTypes.Literal, "-12"));
            expected.Add(new CalculatorToken(TokenTypes.Operator, "*"));
            expected.Add(new CalculatorToken(TokenTypes.Literal, "34"));

            // Act
            // Assert
            var actual = CalculatorParser.Tokenize(arr, operators);

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void NegativeExpression2()
        {
            // Arrange
            char[] arr = { '-', '1', '2', '-', '-', '4' };

            List<CalculatorToken> expected = new List<CalculatorToken>();
            expected.Add(new CalculatorToken(TokenTypes.Literal, "-12"));
            expected.Add(new CalculatorToken(TokenTypes.Operator, "-"));
            expected.Add(new CalculatorToken(TokenTypes.Literal, "-4"));

            // Act
            // Assert
            var actual = CalculatorParser.Tokenize(arr, operators);

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DotExpression1()
        {
            // Arrange
            char[] arr = { '-', '1', '2', '.', '3', '+', '5', '.', '6', '6', '6', };

            List<CalculatorToken> expected = new List<CalculatorToken>();
            expected.Add(new CalculatorToken(TokenTypes.Literal, "-12.3"));
            expected.Add(new CalculatorToken(TokenTypes.Operator, "+"));
            expected.Add(new CalculatorToken(TokenTypes.Literal, "5.666"));

            // Act
            // Assert
            var actual = CalculatorParser.Tokenize(arr, operators);

            CollectionAssert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void OperatorOnFirstCharacter_ShouldThrowArgumentException()
        {
            // Arrange
            char[] arr = { '*', '1', '2', '*', '3', '4' };

            // Act
            // Assert
            Assert.ThrowsException<System.ArgumentException>(() =>
                    CalculatorParser.Tokenize(arr, operators));

        }


        //[TestMethod]
        //public void Expression_0()
        //{
        //    // Arrange
        //    double expected = 615;
        //    string exp = "123 * 5";
        //    CalculatorParser parser = new CalculatorParser(operators);

        //    // Act
        //    // Assert
        //    double actual = parser.Parse(exp);
        //    Assert.AreEqual(expected, actual, 0, "Wrong answer");
        //}
    }
}
