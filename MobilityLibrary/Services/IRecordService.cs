using MobilityLibrary.Services.DTOs.Request;
using MobilityLibrary.Services.DTOs.Response;

namespace MobilityLibrary.Services;

public interface IRecordService
{
    Task<List<RecordResponse>> GetAllRecordsAsync();
    Task<(bool Success, string Message)> AddRecordAsync(RecordRequest request);
    Task<(bool Success, string Message)> DeleteRecordAsync(string name);
}
