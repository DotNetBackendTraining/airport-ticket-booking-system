using AirportTicketBookingSystem.Application.Service;
using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Contract;
using AirportTicketBookingSystem.Domain.Repository;
using AirportTicketBookingSystem.Domain.Service;
using AirportTicketBookingSystem.Infrastructure.Converter;
using AirportTicketBookingSystem.Infrastructure.Repository;
using AirportTicketBookingSystem.Infrastructure.Service;
using Microsoft.Extensions.DependencyInjection;

namespace AirportTicketBookingSystem.Presentation;

public static class DependencyInjector
{
    private static string GetVariableOrThrow(string envVariable)
    {
        var variable = Environment.GetEnvironmentVariable(envVariable);
        if (variable == null)
            throw new KeyNotFoundException($"Environment variable '{envVariable}' not found");
        return variable;
    }

    private static void InjectDatabaseServices(IServiceCollection services)
    {
        services.AddSingleton<ICsvEntityConverter<Flight>, FlightConverter>();
        services.AddSingleton<ICsvEntityConverter<Booking>, BookingConverter>();
        services.AddSingleton<ICsvEntityConverter<Airport>, AirportConverter>();
        services.AddSingleton<ICsvEntityConverter<Passenger>, PassengerConverter>();

        services.AddSingleton<IFileService<Flight>, CsvFileService<Flight>>(provider =>
        {
            var filepath = GetVariableOrThrow("FLIGHTS_FILE_PATH");
            var converter = provider.GetRequiredService<ICsvEntityConverter<Flight>>();
            return new CsvFileService<Flight>(filepath, converter);
        });
        services.AddSingleton<IFileService<Booking>, CsvFileService<Booking>>(provider =>
        {
            var filepath = GetVariableOrThrow("BOOKS_FILE_PATH");
            var converter = provider.GetRequiredService<ICsvEntityConverter<Booking>>();
            return new CsvFileService<Booking>(filepath, converter);
        });
        services.AddSingleton<IFileService<Passenger>, CsvFileService<Passenger>>(provider =>
        {
            var filepath = GetVariableOrThrow("PASSENGERS_FILE_PATH");
            var converter = provider.GetRequiredService<ICsvEntityConverter<Passenger>>();
            return new CsvFileService<Passenger>(filepath, converter);
        });
        services.AddSingleton<IFileService<Airport>, CsvFileService<Airport>>(provider =>
        {
            var filepath = GetVariableOrThrow("AIRPORTS_FILE_PATH");
            var converter = provider.GetRequiredService<ICsvEntityConverter<Airport>>();
            return new CsvFileService<Airport>(filepath, converter);
        });
    }

    private static void InjectRepositoryServices(IServiceCollection services)
    {
        services.AddSingleton<ISimpleDatabaseService<Flight>, SimpleDatabaseService<Flight>>();
        services.AddSingleton<ISimpleDatabaseService<Booking>, SimpleDatabaseService<Booking>>();
        services.AddSingleton<ISimpleDatabaseService<Passenger>, SimpleDatabaseService<Passenger>>();
        services.AddSingleton<ISimpleDatabaseService<Airport>, SimpleDatabaseService<Airport>>();

        services.AddSingleton<IFlightRepository, FlightRepository>();
        services.AddSingleton<IBookingRepository, BookingRepository>();
        services.AddSingleton<IPassengerRepository, PassengerRepository>();
        services.AddSingleton<IAirportRepository, AirportRepository>();
    }

    private static void InjectClientServices(IServiceCollection services)
    {
        services.AddSingleton<IGlobalService, GlobalService>();
        services.AddSingleton<IClientService, ClientService>();
    }

    private static void InjectManagerServices(IServiceCollection services)
    {
        services.AddSingleton<IGlobalService, GlobalService>();
        services.AddSingleton<IManagerService, ManagerService>();
    }

    public static ServiceProvider CreateClientServiceProvider()
    {
        var services = new ServiceCollection();
        InjectDatabaseServices(services);
        InjectRepositoryServices(services);
        InjectClientServices(services);
        return services.BuildServiceProvider();
    }

    public static ServiceProvider CreateManagerServiceProvider()
    {
        var services = new ServiceCollection();
        InjectDatabaseServices(services);
        InjectRepositoryServices(services);
        InjectManagerServices(services);
        return services.BuildServiceProvider();
    }
}