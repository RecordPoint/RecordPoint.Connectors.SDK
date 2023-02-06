namespace ReferenceConnector.Common
{
    /// <summary>
    /// A helper to determine if an action should be performed based on a simple probability algorithm
    /// </summary>
    public interface IRandomHelper
    {
        bool CalculateLikelihood(int likelihood);
        int GenerateRandomInteger(int min, int max);
        string GenerateRandomWord();
        string GenerateRandomWord(int length);
        string GenerateRandomWords(int numWords);

        DateTimeOffset GenerateRandomDateTimeOffset(DateTimeOffset minDate);
        DateTimeOffset GenerateRandomDateTimeOffset(DateTimeOffset minDate, DateTimeOffset maxDate);
    }
}
