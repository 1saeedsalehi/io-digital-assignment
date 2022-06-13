using IO.TedTalk.Api;
using IO.TedTalk.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace IO.TedTalk.Api.Controllers;

public class SampleController : ApiControllerBase
{
    private readonly SampleService _sampleService;
    private readonly ILogger<SampleController> _logger;
    private readonly Settings _settings;

    public SampleController(
        SampleService sampleService,
        IOptions<Settings> options,
        ILogger<SampleController> logger)
    {
        _sampleService = sampleService;
        _logger = logger;
        _settings = options.Value;
    }
    /// <summary>
    /// sample api!
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost("ping")]
    public IActionResult Ping(string input)
    {
        //call service
        var response = _sampleService.Ping(input);

        //Log sample
        _logger.LogInformation(response);

        //read setting
        var settingValue = _settings.Test;

        return Ok(response);
    }
}
