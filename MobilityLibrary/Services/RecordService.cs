using Microsoft.Extensions.Logging;
using MobilityLibrary.Entities;
using MobilityLibrary.Repositories;
using MobilityLibrary.Services.DTOs.Request;
using MobilityLibrary.Services.DTOs.Response;

namespace MobilityLibrary.Services;

public class RecordService : IRecordService
{
    private readonly IRecordRepository _recordRepository;
    private readonly ILogger<RecordService> _logger;

    public RecordService(IRecordRepository recordRepository, ILogger<RecordService> logger)
    {
        _recordRepository = recordRepository;
        _logger = logger;
    }

    public async Task<List<RecordResponse>> GetAllRecordsAsync()
    {
        _logger.LogInformation("Getting all records");
        var result = await _recordRepository.ReadAllAsync();
        if (result == null)
        {
            _logger.LogError("No records found");
            return new List<RecordResponse>();
        }
        _logger.LogInformation($"Found {result.Count} records");
        return result.Select(x => new RecordResponse(x.Id, x.FirstName, x.LastName, x.Age)).ToList();
    }

    public async Task<(bool Success, string Message)> AddRecordAsync(RecordRequest request)
    {
        var record = new Record
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Age = request.Age
        };
        _logger.LogInformation($"Creating record with name {record.FirstName} {record.LastName}");
        var result = await _recordRepository.CreateAsync(record);
        if (result == null)
        {
            _logger.LogError($"Failed to create record with name {record.FirstName} {record.LastName}");
            return (false, $"Failed to create record with name {record.FirstName} {record.LastName}");
        }
        _logger.LogInformation($"Created record with name {record.FirstName} {record.LastName}");
        return (true, $"Created record with name {record.FirstName} {record.LastName}");
    }

    public async Task<(bool Success, string Message)> DeleteRecordAsync(string name)
    {
        if(string.IsNullOrWhiteSpace(name))
        {
            _logger.LogError($"Failed to delete record with id {name}");
            return (false, $"Failed to delete record with id {name}");
        }
        _logger.LogInformation($"Deleting record with id {name}");
        var result = await _recordRepository.DeleteAsync(name);
        if (!result)
        {
            _logger.LogError($"Failed to delete record with id {name}");
            return (false, $"Failed to delete record with id {name}");
        }
        _logger.LogInformation($"Deleted record with id {name}");
        return (true, $"Deleted record with id {name}");
    }
}
