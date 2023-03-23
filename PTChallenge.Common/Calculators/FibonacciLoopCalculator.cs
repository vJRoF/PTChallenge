using System.Numerics;
using Microsoft.Extensions.Caching.Memory;

namespace PTChallenge.Common.Calculators;

public class FibonacciLoopCalculator : IFibonacciCalculator
{
    private readonly IMemoryCache _memoryCache;

    public FibonacciLoopCalculator(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }
    
    public BigInteger Calculate(BigInteger n)
    {
        BigInteger current = 0;

        if (n == 0)
            current = 0;

        if (n == 1)
            current = 1;

        if (_memoryCache.TryGetValue(CacheKey(n - 2), out BigInteger preparent) &&
            _memoryCache.TryGetValue(CacheKey(n - 1), out BigInteger parent))
        {
            //считаем и задаём новые значение кэша на основании значений из кэша
            current = preparent + parent;
            
        }
        else
        {
            // пересчитываем, кэш почему-то сломался
            current = CalculateFromBeginning(n);
        }
        
        _memoryCache.Set(CacheKey(n), current);
        return current;
    }

    /// <summary>
    ///     Пересчитать значение i-ого члена последовательности сначала (без использования кэша)
    /// </summary>
    /// <param name="i">Номер члена последовательности</param>
    private BigInteger CalculateFromBeginning(BigInteger i)
    {
        if (i == 0)
            return 0;
        
        if (i == 1)
            return 1;

        BigInteger preparent = 0;
        BigInteger parent = 1;
        for (BigInteger k = 2; k <= i; k++)
        {
            var current = preparent + parent;
            if (k == i)
                return current;
            
            preparent = parent;
            parent = current;
        }
        
        throw new InvalidOperationException("never thrown");
    }

    private string CacheKey(BigInteger i)
        => $"fib:{i}";
}