using System.Numerics;

namespace PTChallenge.Common.Calculators;

public interface IFibonacciCalculator
{
    BigInteger Calculate(BigInteger n);
}