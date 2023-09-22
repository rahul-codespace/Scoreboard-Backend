using Scoreboard.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scoreboard.Repository.SubmissionComments
{
    public interface ISubmissionCommentRepository
    {
        Task<List<SubmissionComment>> AddListAsync(List<SubmissionComment> submissionComments);
    }
}
