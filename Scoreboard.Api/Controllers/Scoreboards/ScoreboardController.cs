using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scoreboard.Contracts.Scoreboards;
using Scoreboard.Repository.Scoreboards;

namespace Scoreboard.Api.Controllers.Scoreboards;

[ApiController]
[Route("api/[controller][Action]")]
public class ScoreboardController : ControllerBase
{
    private readonly IScoreboardRepository _scoreboardRepository;

    public ScoreboardController(IScoreboardRepository scoreboardRepository)
    {
        _scoreboardRepository = scoreboardRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<StudentInfoDto>>> GetStudentsInfoAsync()
    {
        var studentInfo = await _scoreboardRepository.GetStudentsInfo();
        if (studentInfo == null)
        {
            return BadRequest($"Student info not found");
        }
        return Ok(studentInfo);
    }

    [HttpGet]
    public async Task<ActionResult> GetStudentsInfoByStreamAsync(int studentId)
    {
        var studentInfo = await _scoreboardRepository.GetStudentsInfo(studentId);
        if (studentInfo == null)
        {
            return BadRequest($"Student info not found");
        }
        return Ok(studentInfo);
    }

    [HttpGet]
    [Authorize(Roles = "HR,TL,Mentor")]
    public async Task<ActionResult> GetStudentsInfoWithFeedbackAsync(int studentId)
    {
        var studentInfo = await _scoreboardRepository.GetStudentsInfoWithFeedback(studentId);
        if (studentInfo == null)
        {
            return BadRequest($"Student info not found");
        }
        return Ok(studentInfo);
    }

}
