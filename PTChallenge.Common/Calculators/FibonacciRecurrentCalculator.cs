using System.Numerics;

namespace PTChallenge.Common.Calculators;

public class FibonacciRecurrentCalculator : IFibonacciCalculator
{
    private Dictionary<BigInteger, BigInteger> cache = new ();
    public BigInteger Calculate(BigInteger n)
    {
        if (cache.ContainsKey(n))
            return cache[n];
        
        if (n == 0) return 0;
        if (n == 1) return 1;
        var result = Calculate(n - 1) + Calculate(n - 2);
        cache.Add(n, result);
        return result;
    }
}