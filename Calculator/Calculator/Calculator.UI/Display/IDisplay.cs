namespace Calculator.UI.Display
{
    public interface IDisplay
    {
        string GetCalculationFromUser();

        void DisplayAnswer(string answer);
    }
}
