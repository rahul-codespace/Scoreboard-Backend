using Scoreboard.Contracts.Canvas.ResponseDto;
using Scoreboard.Contracts.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scoreboard.Service.Canvas
{
    public interface IGetStudentDataServices
    {
        Task<StudentAssessmentApiResponseDto> GetStudentAssignment();
        Task<List<StudentDto>> GetStudentsDataFromCanvas(List<StudentDto> students);
        Task<List<GetStudentDataDto>> GetStudentData(String email);
    }
}
