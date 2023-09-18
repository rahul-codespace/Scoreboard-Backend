using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scoreboard.Repository.Streams
{
    public interface IStreamRepository
    {
        Task<Domain.Models.Stream?> GetStreamAsync(int id);
        Task<List<Domain.Models.Stream>> GetStreamsAsync();
        Task<Domain.Models.Stream> AddStreamAsync(Domain.Models.Stream stream);
        Task<List<Domain.Models.Stream>> AddStreamListAsync(List<Domain.Models.Stream> streams);
        Task<Domain.Models.Stream> UpdateStreamAsync(Domain.Models.Stream stream);
        Task<Domain.Models.Stream?> DeleteStreamAsync(int id);
    }
}
