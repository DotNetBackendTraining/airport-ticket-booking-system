using AirportTicketBookingSystem.Application.Interfaces;
using AirportTicketBookingSystem.Application.Interfaces.Service;
using AirportTicketBookingSystem.Application.Interfaces.Service.Request;
using AirportTicketBookingSystem.Application.Service;
using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Criteria.Search;
using AirportTicketBookingSystem.Domain.Interfaces.Repository;
using AirportTicketBookingSystem.Domain.Interfaces.Service;
using AirportTicketBookingSystem.Infrastructure.Converter;
using AirportTicketBookingSystem.Infrastructure.Interfaces;
using AirportTicketBookingSystem.Infrastructure.Repository;
using AirportTicketBookingSystem.Infrastructure.Service;
using AirportTicketBookingSystem.Infrastructure.Service.Database;
using AirportTicketBookingSystem.Infrastructure.Service.Database.RelationalLayers;
using AirportTicketBookingSystem.Infrastructure.Service.Filters;
using AirportTicketBookingSystem.Presentation.Controller;
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
            var filepath = GetVariableOrThrow("BOOKINGS_FILE_PATH");
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

        services.AddSingleton<IValidationService, ValidationService>();

        services.AddSingleton<DatabasePersistenceService<Flight>>();
        services.AddSingleton<DatabasePersistenceService<Booking>>();
        services.AddSingleton<DatabasePersistenceService<Passenger>>();
        services.AddSingleton<DatabasePersistenceService<Airport>>();

        services.AddSingleton<IQueryDatabaseService<Flight>, DatabasePersistenceService<Flight>>();
        services.AddSingleton<IQueryDatabaseService<Booking>, DatabasePersistenceService<Booking>>();
        services.AddSingleton<IQueryDatabaseService<Passenger>, DatabasePersistenceService<Passenger>>();
        services.AddSingleton<IQueryDatabaseService<Airport>, DatabasePersistenceService<Airport>>();

        services.AddSingleton<ICrudDatabaseService<Flight>>(provider =>
        {
            var baseService = provider.GetRequiredService<DatabasePersistenceService<Flight>>();
            var relationalLayer = new FlightRelationalLayer(
                baseService,
                provider.GetRequiredService<IQueryDatabaseService<Airport>>(),
                provider.GetRequiredService<IQueryDatabaseService<Booking>>());
            var uniquenessLayer = new DatabaseUniquenessLayer<Flight>(baseService, relationalLayer);
            return new DatabaseValidationLayer<Flight>(
                uniquenessLayer,
                provider.GetRequiredService<IValidationService>());
        });
        services.AddSingleton<ICrudDatabaseService<Booking>, DatabaseValidationLayer<Booking>>(provider =>
        {
            var baseService = provider.GetRequiredService<DatabasePersistenceService<Booking>>();
            var relationalLayer = new BookingRelationalLayer(
                baseService,
                provider.GetRequiredService<IQueryDatabaseService<Flight>>(),
                provider.GetRequiredService<IQueryDatabaseService<Passenger>>());
            var uniquenessLayer = new DatabaseUniquenessLayer<Booking>(baseService, relationalLayer);
            return new DatabaseValidationLayer<Booking>(
                uniquenessLayer,
                provider.GetRequiredService<IValidationService>());
        });
        services.AddSingleton<ICrudDatabaseService<Passenger>, DatabaseValidationLayer<Passenger>>(provider =>
        {
            var baseService = provider.GetRequiredService<DatabasePersistenceService<Passenger>>();
            var relationalLayer = new PassengerRelationalLayer(
                baseService,
                provider.GetRequiredService<IQueryDatabaseService<Booking>>());
            var uniquenessLayer = new DatabaseUniquenessLayer<Passenger>(baseService, relationalLayer);
            return new DatabaseValidationLayer<Passenger>(
                uniquenessLayer,
                provider.GetRequiredService<IValidationService>());
        });
        services.AddSingleton<ICrudDatabaseService<Airport>, DatabaseValidationLayer<Airport>>(provider =>
        {
            var baseService = provider.GetRequiredService<DatabasePersistenceService<Airport>>();
            var relationalLayer = new AirportRelationalLayer(
                baseService,
                provider.GetRequiredService<IQueryDatabaseService<Flight>>());
            var uniquenessLayer = new DatabaseUniquenessLayer<Airport>(baseService, relationalLayer);
            return new DatabaseValidationLayer<Airport>(
                uniquenessLayer,
                provider.GetRequiredService<IValidationService>());
        });
    }

    private static void InjectRepositoryServices(IServiceCollection services)
    {
        services.AddSingleton<IFlightRepository, FlightRepository>();
        services.AddSingleton<IBookingRepository, BookingRepository>();
        services.AddSingleton<IPassengerRepository, PassengerRepository>();
        services.AddSingleton<IAirportRepository, AirportRepository>();

        services.AddSingleton<IFilteringService<Airport, AirportSearchCriteria>, AirportFilteringService>();
        services.AddSingleton<IFilteringService<Flight, FlightSearchCriteria>, FlightFilteringService>();
        services.AddSingleton<IFilteringService<Booking, BookingSearchCriteria>, BookingFilteringService>();

        services.AddSingleton<IFlightService, FlightService>();
        services.AddSingleton<IBookingService, BookingService>();
        services.AddSingleton<IPassengerService, PassengerService>();
        services.AddSingleton<IAirportService, AirportService>();
    }

    private static void InjectClientServices(IServiceCollection services)
    {
        services.AddSingleton<IBookingManagementService, BookingManagementService>();
        services.AddSingleton<IPassengerRegistrationService, PassengerRegistrationService>();

        services.AddSingleton<IGlobalRequestService, GlobalRequestService>();
        services.AddSingleton<IClientRequestService, ClientRequestService>();

        services.AddSingleton<GlobalController>();
        services.AddSingleton<ClientController>();
    }

    private static void InjectManagerServices(IServiceCollection services)
    {
        services.AddSingleton<ISearchService, SearchService>();
        services.AddSingleton<IFlightManagementService, FlightManagementService>();
        services.AddSingleton<IReflectionService, ReflectionService>();
        services.AddSingleton<IUploadService<Flight>, CsvUploadService<Flight>>();

        services.AddSingleton<IGlobalRequestService, GlobalRequestService>();
        services.AddSingleton<IManagerRequestService, ManagerRequestService>();

        services.AddSingleton<GlobalController>();
        services.AddSingleton<ManagerController>();
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