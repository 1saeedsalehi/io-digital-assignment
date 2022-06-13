using Io.TedTalk.Core.DTOs;
using Io.TedTalk.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace IO.TedTalk.Api.Controllers;

[ApiVersion("1.0")]
public class TedController : ApiControllerBase
{
    private readonly TedService _tedService;
    private readonly ILogger<TedController> _logger;

    public TedController(
        TedService tedService,
        ILogger<TedController> logger)
    {
        _tedService = tedService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetTedInputDto dto, CancellationToken cancellationToken)
    {
        //if I had more time I will use mediator!
        var result = await _tedService.GetAll(dto, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken)
    {
        //if I had more time I will use mediator!
        var result = await _tedService.GetById(id, cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTedDto dto, CancellationToken cancellationToken)
    {
        //if I had more time I will use mediator!
        var result = await _tedService.Create(dto, cancellationToken);
        return CreatedAtAction(nameof(GetById), result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute]int id,[FromBody] CreateTedDto dto, CancellationToken cancellationToken)
    {
        //if I had more time I will use mediator!
        await _tedService.Update(id,dto, cancellationToken);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove([FromRoute]int id, CancellationToken cancellationToken)
    {
        //if I had more time I will use mediator!
        await _tedService.Delete(id, cancellationToken);
        return Ok();
    }
}
