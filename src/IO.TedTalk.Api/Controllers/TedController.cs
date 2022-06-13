using Io.TedTalk.Core.DTOs;
using Io.TedTalk.Core.Entities;
using Io.TedTalk.Services.Services;
using IO.TedTalk.Api.Framrwork.Models;
using Microsoft.AspNetCore.Mvc;

namespace IO.TedTalk.Api.Controllers;

[ApiVersion("1.0")]
public class TedController : ApiControllerBase
{
    private readonly TedService _tedService;

    public TedController(
        TedService tedService)
    {
        _tedService = tedService;
    }

    /// <summary>
    /// returns a list of teds
    /// </summary>
    /// <param name="dto"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IOApiResponse<IEnumerable<Ted>>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(IOApiResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(IOApiResponse))]
    public async Task<IActionResult> GetAll([FromQuery] GetTedInputDto dto, CancellationToken cancellationToken)
    {
        //if I had more time I will use mediator!
        var result = await _tedService.GetAll(dto, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// returns a ted 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IOApiResponse<Ted>))]
    [ProducesResponseType(StatusCodes.Status404NotFound , Type = typeof(IOApiResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(IOApiResponse))]
    public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken)
    {
        //if I had more time I will use mediator!
        var result = await _tedService.GetById(id, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// creates a new ted 
    /// </summary>
    /// <param name="dto"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IOApiResponse<int>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(IOApiResponse))]
    public async Task<IActionResult> Create([FromBody] CreateTedDto dto, CancellationToken cancellationToken)
    {
        //if I had more time I will use mediator!
        var result = await _tedService.Create(dto, cancellationToken);

        return Ok(result);
        //I've commented this because I need some investigation to handle this with my customized result wrapper
        //return CreatedAtAction(nameof(GetById), new { id = result });
        
    }

    /// <summary>
    ///  updates a ted entity with given id and body
    /// </summary>
    /// <param name="id"></param>
    /// <param name="dto"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IOApiResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(IOApiResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(IOApiResponse))]
    public async Task<IActionResult> Update([FromRoute]int id,[FromBody] CreateTedDto dto, CancellationToken cancellationToken)
    {
        //if I had more time I will use mediator!
        await _tedService.Update(id,dto, cancellationToken);
        return Ok();
    }

    /// <summary>
    /// removes a ted entity with given id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IOApiResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(IOApiResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(IOApiResponse))]
    public async Task<IActionResult> Remove([FromRoute]int id, CancellationToken cancellationToken)
    {
        //if I had more time I will use mediator!
        await _tedService.Delete(id, cancellationToken);
        return Ok();
    }
}
