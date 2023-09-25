﻿using Microsoft.AspNetCore.Mvc;
using Scoreboard.Contracts.Scoreboards;
using Scoreboard.Repository.Scoreboards;

namespace Scoreboard.Api.Controllers.Scoreboards
{
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
    }
}
