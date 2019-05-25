using System;

public static class RandomUtils
{
    public static readonly Random _random = new Random();
    public static float GetRandomNumber(double minimum, double maximum)
    {
        return Convert.ToSingle(_random.NextDouble() * (maximum - minimum) + minimum);
    }
}
