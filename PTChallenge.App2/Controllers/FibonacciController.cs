using System.Numerics;
using Microsoft.AspNetCore.Mvc;
using PTChallenge.Common;

namespace PTChallenge.App2.Controllers;

[ApiController]
[Route("[controller]")]
public class FibonacciController : ControllerBase
{
    private readonly IFibonacciCalculator _calculator;
    private readonly ILogger<FibonacciController> _logger;

    public FibonacciController(
        IFibonacciCalculator calculator,
        ILogger<FibonacciController> logger)
    {
        _calculator = calculator;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Get([FromQuery] string i)
    {
        if (!BigInteger.TryParse(i, out var n))
            return ValidationProblem(detail: $"Неправильный формат числа \"{i}\"");

        Task.Run(() => _calculator.Calculate(n));

        return NoContent();
    }
}