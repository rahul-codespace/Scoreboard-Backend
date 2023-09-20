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
        Task<List<StudentDto>> SeedStudentsDataAsync(List<int> studentIds);
    }
}
