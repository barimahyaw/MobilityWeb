using Microsoft.EntityFrameworkCore;
using MobilityLibrary.Entities;

namespace MobilityLibrary;

public class RecordRepository : IRecordRepository
{
    private readonly ApplicationDbContext _context;
    public RecordRepository(ApplicationDbContext context) => _context = context;

    public async Task<Record> CreateAsync(Record record)
    {
        _context.Records.Add(record);
        await _context.SaveChangesAsync();
        return record;
    }

    public async Task<Record?> ReadAsync(int id) =>
        await _context.Records.FindAsync(id);

    public async Task<Record?> UpdateAsync(Record record)
    {
        _context.Records.Update(record);
        await _context.SaveChangesAsync();
        return record;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var record = await _context.Records.FindAsync(id);
        if (record is null) return false;
        _context.Records.Remove(record);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<Record>> ReadAllAsync() =>
        await _context.Records.ToListAsync();
}
