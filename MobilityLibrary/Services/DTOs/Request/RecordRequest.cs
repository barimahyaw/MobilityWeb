using System.ComponentModel.DataAnnotations;

namespace MobilityLibrary.Services.DTOs.Request;

public class RecordRequest
{
    [Required(ErrorMessage = "first name is required")]
    public string FirstName { get; set; } = null!;
    [Required(ErrorMessage = "last name is required")]
    public string LastName { get; set; } = null!;
    [Range(1, int.MaxValue, ErrorMessage = "age must be greater than 0")]
    public int Age { get; set; }
}
