using System.Numerics;
using Microsoft.AspNetCore.Mvc;

namespace PTChallenge.Controllers;

[ApiController]
[Route("[controller]")]
public class FibonacciController : ControllerBase
{

    private readonly ILogger<FibonacciController> _logger;

    public FibonacciController(ILogger<FibonacciController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<WeatherForecast> Get(BigInteger arg)
    {
    }
}