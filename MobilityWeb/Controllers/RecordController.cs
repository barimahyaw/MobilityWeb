using Microsoft.AspNetCore.Mvc;
using MobilityLibrary.Services;
using MobilityLibrary.Services.DTOs.Request;

namespace MobilityWeb.Controllers;

public class RecordController : Controller
{
    private readonly IRecordService _recordService;
    public RecordController(IRecordService recordService) => _recordService = recordService;

    public async Task<IActionResult> List()
    {
        var records = await _recordService.GetAllRecordsAsync();
        return View(records);
    }

    [HttpGet]
    public IActionResult Add() => View();

    [HttpPost]
    public async Task<IActionResult> Add(RecordRequest record)
    {
        if (!ModelState.IsValid)
        {
            return View(record);
        }
        var (Success, Message) = await _recordService.AddRecordAsync(record);
        if (!Success)
        {
            ModelState.AddModelError("Error", Message);
            return View(record);
        }
        return RedirectToAction(nameof(List));
    }

    [HttpGet]
    public IActionResult Delete() => View();

    [HttpPost]
    public async Task<IActionResult> Delete(string name)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        var (Success, Message) = await _recordService.DeleteRecordAsync(name);
        if (!Success)
        {
            ModelState.AddModelError("Error", Message);
            return View();
        }
        return RedirectToAction(nameof(List));
    }
}
