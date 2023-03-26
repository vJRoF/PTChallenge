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
    private readonly WorkerPool _workerPool;

    public FibonacciController(
        ILogger<FibonacciController> logger,
        WorkerPool workerPool)
    {
        _logger = logger;
        _workerPool = workerPool;
    }

    [HttpPost("/api/fibonacci/calculate")]
    public IActionResult Get([FromBody] NumberMessageModel messageModel)
    {
        if (!BigInteger.TryParse(messageModel.Number, out var n))
        {
            _logger.LogError(55466, "Прислали непонятно что: {Number}", messageModel.Number);
            return ValidationProblem(detail: $"Неправильный формат числа \"{messageModel.Number}\"");
        }

        var worker = _workerPool.GetOrCreate(messageModel.ChainId);
#pragma warning disable CS4014
        
        worker.CalculateAndSendAsync(n, HttpContext.RequestAborted);
#pragma warning restore CS4014

        return NoContent();
    }
}