using Scoreboard.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scoreboard.Repository.StudentTotalPoints;

public interface IStudentTotalPointRepository
{
    Task<StudentTotalPoint> AddStudentTotalPointAsync(int studentId);
}
