using Microsoft.AspNetCore.Mvc;
using MobilityLibrary.Services.DTOs.Request;
using MobilityLibrary.Services.DTOs.Response;

namespace MobilityWeb.Controllers;

public class ServiceRecordController : Controller
{
    private readonly HttpClient _httpClient;
    public ServiceRecordController(HttpClient httpClient) => _httpClient = httpClient;

    [HttpGet]
    public async Task<IActionResult> List()
    {
        var response = await _httpClient.GetAsync("api/record");
        if (!response.IsSuccessStatusCode) return View();
        var records = await response.Content.ReadFromJsonAsync<IEnumerable<RecordResponse>>();
        return View(records);
    }

    [HttpGet]
    public IActionResult Add() => View();

    [HttpPost]
    public async Task<IActionResult> Add(RecordRequest record)
    {
        if (!ModelState.IsValid) return View(record);
        var response = await _httpClient.PostAsJsonAsync("api/record", record);
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            ViewBag.ErrorMessage = content.ToString();
            return View(record);
        }
        return RedirectToAction(nameof(List));
    }

    [HttpGet]
    public IActionResult Delete() => View();

    [HttpPost]
    public async Task<IActionResult> Delete(string name)
    {
        if (!ModelState.IsValid) return View();
        var response = await _httpClient.DeleteAsync($"api/record?name={name}");
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            ViewBag.ErrorMessage = content.ToString();
            return View();
        }
        ViewBag.SuccessMessage = content.ToString();
        return View();
    }
}
