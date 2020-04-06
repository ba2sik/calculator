namespace Calculator.UI.Abstraction
{
    interface IDisplay
    {
        string GetCalculationFromUser();

        void DisplayAnswer(float answer);
    }
}
