using System.Numerics;
using PTChallenge.Common;
using PTChallenge.Common.Calculators;

namespace PTChallenge.Tests;

public class UnitTest1
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void FibonacciRecurrentCalculator_Ok(BigInteger n, string answer)
    {
        var calculator = new FibonacciRecurrentCalculator();

        var result = calculator.Calculate(n);
        
        Assert.Equal(BigInteger.Parse(answer), result);
    }
    
    [Theory]
    [MemberData(nameof(GetCases))]
    public void FibonacciLoopCalculator_Ok(BigInteger n, string answer)
    {
        var calculator = new FibonacciRecurrentCalculator();

        var result = calculator.Calculate(n);
        
        Assert.Equal(BigInteger.Parse(answer), result);
    }
    
    [Theory]
    [MemberData(nameof(GetCases))]
    public void FibonacciBinetCalculator_Ok(BigInteger n, string answer)
    {
        var calculator = new FibonacciBinetCalculator();

        var result = calculator.Calculate(n);
        
        Assert.Equal(BigInteger.Parse(answer), result);
    }

    public static IEnumerable<object[]> GetCases()
    {
        yield return new object[] {0, 0};
        yield return new object[] {1, 1};
        yield return new object[] {2, 1};
        yield return new object[] {3, 2};
        yield return new object[] {4, 3};
        yield return new object[] {5, 5};
        yield return new object[] {6, 8};
        yield return new object[] {7, 13};
        yield return new object[] {8, 21};
        yield return new object[] {9, 34};
        yield return new object[] {10, 55};
        yield return new object[] {11, 89};
        yield return new object[] {12, 144};
        yield return new object[] {200, "280571172992510140037611932413038677189525"};
        yield return new object[] {1000, "43466557686937456435688527675040625802564660517371780402481729089536555417949051890403879840079255169295922593080322634775209689623239873322471161642996440906533187938298969649928516003704476137795166849228875"};
    }
}