using Microsoft.EntityFrameworkCore;
using Scoreboard.Data.Context;

namespace Scoreboard.Repository.Streams;

public class StreamRepository : IStreamRepository
{
    private readonly ScoreboardDbContext _context;

    public StreamRepository(ScoreboardDbContext context)
    {
        _context = context;
    }

    public async Task<Domain.Models.Stream?> GetStreamAsync(int id)
    {
        return await _context.Streams.FirstOrDefaultAsync(s => s.Id == id);
    }
    public async Task<List<Domain.Models.Stream>> GetStreamsAsync()
    {
        return await _context.Streams.ToListAsync();
    }
    public async Task<Domain.Models.Stream> AddStreamAsync(Domain.Models.Stream stream)
    {
        var result = await _context.Streams.FirstOrDefaultAsync(s => s.Id == stream.Id);
        if (result == null)
        {
            _context.Streams.Add(stream);
        }
        await _context.SaveChangesAsync();
        return stream;

    }
    public async Task<List<Domain.Models.Stream>> AddStreamListAsync(List<Domain.Models.Stream> streams)
    {
        foreach (var stream in streams)
        {
            var result = await _context.Streams.FirstOrDefaultAsync(s => s.Id == stream.Id);
            if (result == null)
            {
                _context.Streams.Add(stream);
            }
        }
        await _context.SaveChangesAsync();
        return streams;
    }
    public async Task<Domain.Models.Stream> UpdateStreamAsync(Domain.Models.Stream stream)
    {
        var result = await _context.Streams.FirstOrDefaultAsync(s => s.Id == stream.Id);
        if (result != null)
        {
            result.Name = stream.Name;
            await _context.SaveChangesAsync();
        }
        return stream;
    }
    public async Task<Domain.Models.Stream?> DeleteStreamAsync(int id)
    {
        var result = await _context.Streams.FirstOrDefaultAsync(s => s.Id == id);
        if (result != null)
        {
            _context.Streams.Remove(result);
            await _context.SaveChangesAsync();
        }
        return result;
    }
}
