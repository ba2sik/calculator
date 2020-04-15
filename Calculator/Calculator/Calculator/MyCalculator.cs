using Calculator.Core.Parser;
using Calculator.UI.Abstraction;
using System;

namespace Calculator
{
    internal class MyCalculator
    {
        private readonly IDisplay _displayType;
        private readonly IParser _parserType;
        private string _userCalculation = "";
        private double _answer;

        public MyCalculator(IDisplay displayType, IParser parserType)
        {
            _displayType = displayType;
            _parserType = parserType;
            _answer = 0;
        }

        public void GetCalculationFromUser()
        {
            _userCalculation = _displayType.GetCalculationFromUser();
        }

        public void CalculateAnswer()
        {
            try
            {
                _answer = _parserType.Parse(_userCalculation);
            }
            // TODO: Catch MY exception and print them
            catch (Exception e)
            {
                Console.WriteLine("An error occured:");
                Console.WriteLine(e.Message);
            }

        }

        public void DisplayAnswer()
        {
            _displayType.DisplayAnswer(_answer);
        }
    }
}
