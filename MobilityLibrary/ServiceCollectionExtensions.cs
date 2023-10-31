using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MobilityLibrary.Repositories;
using MobilityLibrary.Services;

namespace MobilityLibrary;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection GetDataBaseDbContextConfig(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        return services.AddDbContext<ApplicationDbContext>(options => options.ConfigureDbContextOptions(configuration));
    }

    private static void ConfigureDbContextOptions(
        this DbContextOptionsBuilder options,
        IConfiguration configuration)
        => options.UseNpgsql(Environment.GetEnvironmentVariable("CONNECTION_STRING")
            ?? configuration.GetConnectionString("CoreServiceConnectionString"));

    public static IServiceCollection AddRecordRepository(this IServiceCollection services)
        => services.AddScoped<IRecordRepository, RecordRepository>();

    public static IServiceCollection AddRecordService(this IServiceCollection services)
        => services.AddScoped<IRecordService, RecordService>();

    public static IServiceCollection AddHttpClientService(this IServiceCollection services, IConfiguration configuration)
    {
        var url = Environment.GetEnvironmentVariable("SERVICE_BASE_URL") ?? configuration["Service_BaseUrl"];
        services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(url!) });
        return services;
    }
}
