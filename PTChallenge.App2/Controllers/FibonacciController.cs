using System.Numerics;
using Microsoft.AspNetCore.Mvc;
using PTChallenge.App1;
using PTChallenge.Common;

namespace PTChallenge.App2.Controllers;

[ApiController]
[Route("[controller]")]
public class FibonacciController : ControllerBase
{
    private readonly ILogger<FibonacciController> _logger;
    private readonly Worker _worker;

    public FibonacciController(
        ILogger<FibonacciController> logger,
        Worker worker)
    {
        _logger = logger;
        _worker = worker;
    }

    [HttpGet]
    public IActionResult Get([FromQuery] string i)
    {
        if (!BigInteger.TryParse(i, out var n))
        {
            _logger.LogError(55466, "Прислали непонятно что: {Number}", i);
            return ValidationProblem(detail: $"Неправильный формат числа \"{i}\"");
        }

#pragma warning disable CS4014
        _worker.CalculateAndSendAsync(n, HttpContext.RequestAborted);
#pragma warning restore CS4014

        return NoContent();
    }
}