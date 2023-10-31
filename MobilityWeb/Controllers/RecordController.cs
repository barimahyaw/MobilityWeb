using Microsoft.AspNetCore.Mvc;
using MobilityLibrary;
using MobilityLibrary.Entities;
using MobilityWeb.Models;

namespace MobilityWeb.Controllers;

public class RecordController : Controller
{
    private readonly ILogger<RecordController> _logger;
    private readonly IRecordRepository _recordRepository;

    public RecordController(ILogger<RecordController> logger, IRecordRepository recordRepository)
    {
        _logger = logger;
        _recordRepository = recordRepository;
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        _logger.LogInformation("Getting all records");
        var result = await _recordRepository.ReadAllAsync();
        if (result == null)
        {
            _logger.LogError("No records found");
            return View(new List<RecordResponse>());
        }
        _logger.LogInformation($"Found {result.Count} records");
        var response = result.Select(x => new RecordResponse(x.Id, x.FirstName, x.LastName, x.Age)).ToList();
        return View(response);
    }

    [HttpGet]
    public IActionResult Add() => View();

    [HttpPost]
    public async Task<IActionResult> Add(RecordRequest recordRequest)
    {
        var record = new Record
        {
            FirstName = recordRequest.FirstName,
            LastName = recordRequest.LastName,
            Age = recordRequest.Age
        };
        _logger.LogInformation($"Creating record with name {record.FirstName} {record.LastName}");
        var result = await _recordRepository.CreateAsync(record);
        if (result == null)
        {
            _logger.LogError($"Failed to create record with name {record.FirstName} {record.LastName}");
            ModelState.AddModelError("Name", "Failed to create record");
            return BadRequest();
        }
        _logger.LogInformation($"Created record with name {record.FirstName} {record.LastName}");
        ViewBag.Message = $"Created record with name {record.FirstName} {record.LastName}";
        return RedirectToAction(nameof(List));
    }

    [HttpGet]
    public IActionResult Delete() => View();

    [HttpPost]
    public async Task<IActionResult> Delete(string name)
    {
        if(string.IsNullOrWhiteSpace(name))
        {
            ViewBag.ErrorMessage = "Name is required";
            return View();
        }
        _logger.LogInformation($"Deleting record with name {name}");
        var result = await _recordRepository.DeleteAsync(name);
        if (!result)
        {
            ViewBag.ErrorMessage = $"Failed to delete record with name {name}";
            _logger.LogError($"Failed to delete record with name {name}");
            return View();
        }
        _logger.LogInformation($"Deleted record with name {name}");
        ViewBag.Message = $"Deleted record with name {name}";
        return View();
    }
}
