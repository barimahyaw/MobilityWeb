namespace MobilityLibrary.Entities;

public class Record
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public int Age { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
}
