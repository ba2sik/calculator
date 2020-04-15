using Calculator.Core.Abstraction;
using Calculator.UI.Abstraction;
using System;

namespace Calculator
{
    class MyCalculator
    {
        private readonly IDisplay _displayType = null;
        private readonly IParser _parserType = null;
        private string _userCalculation = "";
        private double _answer = 0.0;

        public MyCalculator(IDisplay displayType, IParser parserType)
        {
            this._displayType = displayType;
            this._parserType = parserType;

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

        public void DisplayAnswwer()
        {
            _displayType.DisplayAnswer(_answer);
        }
    }
}
