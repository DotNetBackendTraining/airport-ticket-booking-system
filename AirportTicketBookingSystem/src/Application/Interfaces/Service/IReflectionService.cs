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
    /// Retrieves all class types within a specified namespace.
    /// </summary>
    /// <param name="namespaceFilter">The namespace to search for class types.</param>
    /// <returns>An enumerable of <see cref="Type"/> objects representing each class found within the specified namespace.</returns>
    IEnumerable<Type> GetClassTypesInNamespace(string namespaceFilter);

    /// <summary>
    /// Retrieves a collection of all domain entities available in the system.
    /// </summary>
    /// <returns>
    /// An enumerable of <see cref="Type"/> objects representing each domain class in the system.
    /// </returns>
    IEnumerable<Type> GetDomainEntityTypes();
}