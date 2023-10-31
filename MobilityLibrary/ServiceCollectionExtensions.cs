﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
}