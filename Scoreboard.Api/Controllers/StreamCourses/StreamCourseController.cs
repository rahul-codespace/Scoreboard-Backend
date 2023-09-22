using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Scoreboard.Domain.Models;
using Scoreboard.Repository.StreamCourses;

namespace Scoreboard.Api.Controllers.StreamCourses
{
    [ApiController]
    [Route("api/[controller][Action]")]
    public class StreamCourseController : ControllerBase
    {
        private readonly IStreamCoursesRepository _streamCoursesRepository;

        public StreamCourseController(IStreamCoursesRepository streamCoursesRepository)
        {
            _streamCoursesRepository = streamCoursesRepository;
        }

        [HttpGet()]
        public async Task<ActionResult<List<StreamCourse>>> GetStreamCourses()
        {
            var result = await _streamCoursesRepository.GetStreamCoursesAsync();
            if (result == null)
            {
                return BadRequest($"Courses not found");
            }
            return Ok(result);
        }

        [HttpGet("{streamId}/{courseId}")]
        public async Task<ActionResult<StreamCourse>> GetStreamCourseAsync(int streamId, int courseId)
        {
            var result = await _streamCoursesRepository.GetStreamCourseAsync(streamId, courseId);
            if (result == null)
            {
                return BadRequest($"Course {courseId} not found");
            }
            return Ok(result);
        }
    }
}
