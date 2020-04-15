using Calculator.Core.Exceptions;
using Calculator.Core.Parser;
using Calculator.UI.Display;
using System;

namespace Calculator
{
    internal class MyCalculator
    {
        private readonly IDisplay _displayType;
        private readonly IParser _parser;
        private string _userCalculation = "";
        private string _answer;

        public MyCalculator(IDisplay display, IParser parser)
        {
            _displayType = display;
            _parser = parser;
            _answer = "";
        }

        public void GetCalculationFromUser()
        {
            _userCalculation = _displayType.GetCalculationFromUser();
        }

        public void CalculateAnswer()
        {
            try
            {
                _answer = _parser.Parse(_userCalculation).ToString();
            }
            catch (ParsingException e)
            {
                _answer = e.Message;
            }
            catch (Exception e)
            {
                _answer = $"An unexpected error occured: {e.Message}";
            }
        }

        public void DisplayAnswer()
        {
            _displayType.DisplayAnswer(_answer);
        }
    }
}
