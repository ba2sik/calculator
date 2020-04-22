using Calculator.Core.Exceptions;
using Calculator.Core.Parser;
using Calculator.UI.Display;
using System;

namespace Calculator
{
    internal class MyCalculator //CR: why did you add interfaces for every class but not for the calculator itself? makes sense to me that there are different types of calculators, like scientific and those crappy basic ones
    {
        private readonly IDisplay _displayType;
        private readonly IParser _parser;
        private string _userCalculation = ""; //CR: why is it initialized here and _answer does not?
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
                _answer = _parser.Parse(_userCalculation).ToString(); //CR: it seems like Parse does not only parse the input, are you sure it's a parser?
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
