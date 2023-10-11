using Scoreboard.Contracts.Scoreboards;
using System.Collections;

namespace Scoreboard.Repository.Scoreboards
{
    ///<summary>
    ///Interface for interacting with the students data source.
    ///</summary>
    public interface IScoreboardRepository
    {
        ///<summary>
        ///Gets the list of all students with their information.
        ///</summary>
        Task<List<StudentInfoDto>> GetStudentsInfo();

        ///<summary>
        ///Gets student information by the given student id.
        ///</summary>
        ///<param name="studentId">The id of the student.</param>
        Task<StudentCourseAssignmentDto> GetStudentsInfo(int studentId);

        ///<summary>
        ///Gets student information with feedback by the given student id.
        ///</summary>
        ///<param name="studentId">The id of the student.</param>
        Task<StudentCourseAssignmentDto> GetStudentsInfoWithFeedback(int studentId);
    }
}