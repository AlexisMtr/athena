namespace Athena.Helpers
{
    public static class NumericsExtension
    {
        public static bool IsBetween(this int value, int lowerBound, int upperBound, bool strict = false)
            => (strict && value > lowerBound && value < upperBound) || value >= lowerBound && value <= upperBound;

        public static bool IsBetween(this long value, long lowerBound, long upperBound, bool strict = false)
            => (strict && value > lowerBound && value < upperBound) || value >= lowerBound && value <= upperBound;

        public static bool IsBetween(this double value, double lowerBound, double upperBound, bool strict = false)
            => (strict && value > lowerBound && value < upperBound) || value >= lowerBound && value <= upperBound;

        public static bool IsBetween(this float value, float lowerBound, float upperBound, bool strict = false)
            => (strict && value > lowerBound && value < upperBound) || value >= lowerBound && value <= upperBound;
    }
}
