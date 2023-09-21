using Microsoft.AspNetCore.Mvc;
using Scoreboard.Repository.Streams;

namespace Scoreboard.Api.Controllers.Streams;

[ApiController]
[Route("api/[controller][Action]")]
public class StreamController : ControllerBase
{
    private readonly IStreamRepository _streamRepository;

    public StreamController(IStreamRepository streamRepository)
    {
        _streamRepository = streamRepository;
    }
    [HttpGet]
    public async Task<ActionResult<List<Domain.Models.Stream>>> GetStreamsAsync()
    {
        var streams = await _streamRepository.GetStreamsAsync();
        if (streams == null)
        {
            return BadRequest($"Streams not found");
        }
        return Ok(streams);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Domain.Models.Stream>> GetStreamAsync(int id)
    {
        var stream = await _streamRepository.GetStreamAsync(id);
        if (stream == null)
        {
            return BadRequest($"Stream {id} not found");
        }
        return Ok(stream);
    }
    [HttpPost]
    public async Task<ActionResult<Domain.Models.Stream>> AddStreamAsync(string StreamName)
    {
        var stream = new Domain.Models.Stream
        {
            Name = StreamName
        };
        var result = await _streamRepository.AddStreamAsync(stream);
        if (result == null)
        {
            return BadRequest($"Stream {stream.Id} not found");
        }
        return Ok(result);
    }
    [HttpPut]
    public async Task<ActionResult<Domain.Models.Stream>> UpdateStreamAsync(Domain.Models.Stream stream)
    {
        var result = await _streamRepository.UpdateStreamAsync(stream);
        if (result == null)
        {
            return BadRequest($"Stream {stream.Id} not found");
        }
        return Ok(result);
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult<Domain.Models.Stream>> DeleteStreamAsync(int id)
    {
        var result = await _streamRepository.DeleteStreamAsync(id);
        if (result == null)
        {
            return BadRequest($"Stream {id} not found");
        }
        return Ok(result);
    }
}
