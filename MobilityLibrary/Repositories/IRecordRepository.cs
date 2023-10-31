using MobilityLibrary.Entities;

namespace MobilityLibrary.Repositories;

public interface IRecordRepository
{
    Task<Record> CreateAsync(Record record);
    Task<bool> DeleteAsync(int id);
    Task<bool> DeleteAsync(string firstName);
    Task<List<Record>> ReadAllAsync();
}
