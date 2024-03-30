namespace AirportTicketBookingSystem.Application.Interfaces.Service;

/// <summary>
/// Provides services for reflecting over types and properties within the application.
/// </summary>
public interface IReflectionService
{
    /// <summary>
    /// Generates a report listing properties and their attributes for a given type.
    /// </summary>
    /// <param name="type">The type to reflect over and report on.</param>
    /// <returns>A formatted string report detailing each property of the specified type and its associated attributes.</returns>
    string ReportPropertiesWithAttributes(Type type);

    /// <summary>
    /// Retrieves a collection of all domain entities available in the system.
    /// </summary>
    /// <returns>
    /// An enumerable of <see cref="Type"/> objects representing each domain class in the system.
    /// </returns>
    IEnumerable<Type> GetDomainEntityTypes();
}