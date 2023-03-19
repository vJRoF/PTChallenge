using System.Numerics;

namespace PTChallenge.Common;

public class FibonacciBinetCalculator : IFibonacciCalculator
{
    public BigInteger Calculate(BigInteger n)
    {
        var binetResult = (Math.Pow((1.0 + Math.Sqrt(5)) / 2, (long) n) - Math.Pow((1.0 - Math.Sqrt(5)) / 2, (long) n)) / Math.Sqrt(5);
        var sresult =$"{Math.Round(binetResult)}";
        return BigInteger.Parse(sresult);
    }
}