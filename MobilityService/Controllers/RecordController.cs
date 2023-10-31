using Microsoft.AspNetCore.Mvc;
using MobilityLibrary.Services;
using MobilityLibrary.Services.DTOs.Request;

namespace MobilityService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RecordController : ControllerBase
{
    private readonly IRecordService _recordService;
    public RecordController(IRecordService recordService) => _recordService = recordService;

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var records = await _recordService.GetAllRecordsAsync();
        return Ok(records);
    }

    [HttpPost]
    public async Task<IActionResult> Post(RecordRequest record)
    {
        var (Success, Message) = await _recordService.AddRecordAsync(record);
        if (!Success) return BadRequest(Message);
        return Ok(Message);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(string name)
    {
        var (Success, Message) = await _recordService.DeleteRecordAsync(name);
        if (!Success) return BadRequest(Message);
        return Ok(Message);
    }
}
