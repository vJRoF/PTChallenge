using System.Numerics;
using Microsoft.AspNetCore.Mvc;
using PTChallenge.Common;
using PTChallenge.Common.Models;

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

    [HttpPost("/api/fibonacci/calculate")]
    public IActionResult Get([FromBody] NumberMessage message)
    {
        if (!BigInteger.TryParse(message.Number, out var n))
        {
            _logger.LogError(55466, "Прислали непонятно что: {Number}", message.Number);
            return ValidationProblem(detail: $"Неправильный формат числа \"{message.Number}\"");
        }

#pragma warning disable CS4014
        _worker.CalculateAndSendAsync(n, HttpContext.RequestAborted);
#pragma warning restore CS4014

        return NoContent();
    }
}