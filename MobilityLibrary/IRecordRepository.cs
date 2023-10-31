using MobilityLibrary.Entities;

namespace MobilityLibrary;

public interface IRecordRepository
{
    Task<Record> CreateAsync(Record record);
    Task<Record?> ReadAsync(int id);
    Task<Record?> UpdateAsync(Record record);
    Task<bool> DeleteAsync(int id);
    Task<List<Record>> ReadAllAsync();
}
