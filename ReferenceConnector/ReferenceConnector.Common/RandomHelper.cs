namespace ReferenceConnector.Common
{
    public class RandomHelper : IRandomHelper
    {
        private readonly Random _random;

        public RandomHelper()
        {
            _random = new Random();
        }

        /// <summary>
        /// Determines if an action should be performed by randomly generating a number between 1 and the upper value
        /// If a 1 is generated, then the action should be performed.
        /// The higher the likelihood value, the lower the probability.
        /// e.g. 
        /// With a likelihood value of 2, there is a 50% probability
        /// With a likelihood value of 20, there is a 5% probability
        /// </summary>
        /// <param name="likelihood"></param>
        /// <returns></returns>
        public bool CalculateLikelihood(int likelihood)
        {
            var rndValue = _random.Next(1, likelihood);
            return rndValue == 1;
        }

        /// <summary>
        /// Generates a random number between the two specified values
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public int GenerateRandomInteger(int min, int max)
        {
            return _random.Next(min, max);
        }

        /// <summary>
        /// Generates a random string between 5 and 15 characters in length
        /// </summary>
        /// <returns></returns>
        public string GenerateRandomWord()
        {
            var length = _random.Next(5, 15);
            return GenerateRandomWord(length);
        }

        /// <summary>
        /// Generates a random string with the specified length
        /// </summary>
        /// <returns></returns>
        public string GenerateRandomWord(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[_random.Next(s.Length)])
                .ToArray());
        }

        /// <summary>
        /// Generates a string with multiple random words seperated by a space.
        /// </summary>
        /// <param name="numWords"></param>
        /// <returns></returns>
        public string GenerateRandomWords(int numWords)
        {
            var strings = new List<string>();
            for (var i = 0; i < numWords; i++)
            {
                strings.Add(GenerateRandomWord());
            }
            return string.Join(' ', strings);
        }

        /// <summary>
        /// Generates a random date between the specified date and the current date time
        /// </summary>
        /// <param name="minDate"></param>
        /// <returns></returns>
        public DateTimeOffset GenerateRandomDateTimeOffset(DateTimeOffset minDate)
        {
            return GenerateRandomDateTimeOffset(minDate, DateTimeOffset.Now);
        }

        /// <summary>
        /// Generates a random date between the specified values
        /// </summary>
        /// <param name="minDate"></param>
        /// <param name="maxDate"></param>
        /// <returns></returns>
        public DateTimeOffset GenerateRandomDateTimeOffset(DateTimeOffset minDate, DateTimeOffset maxDate)
        {
            var range = maxDate.Ticks - minDate.Ticks;
            var rnd = _random.NextInt64(0, range);
            return minDate.AddTicks(rnd);
        }
    }
}
