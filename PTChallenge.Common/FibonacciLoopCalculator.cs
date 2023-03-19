using System.Numerics;

namespace PTChallenge.Common;

public class FibonacciLoopCalculator : IFibonacciCalculator
{
    public BigInteger Calculate(BigInteger n)
    {
        if (n == 0)
            return 0;
        
        if (n == 1)
            return 1;

        BigInteger preparent = 0;
        BigInteger parent = 1;
        for (BigInteger i = 2; i <= n; i++)
        {
            var current = preparent + parent;
            if (i == n)
                return current;
            
            preparent = parent;
            parent = current;
        }

        throw new InvalidOperationException("never thrown");
    }
}