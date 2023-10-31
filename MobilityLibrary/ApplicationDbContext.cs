using Microsoft.EntityFrameworkCore;
using MobilityLibrary.Entities;

namespace MobilityLibrary;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options)
            : base(options) { }

    public DbSet<Record> Records { get; set; } = null!;
}
