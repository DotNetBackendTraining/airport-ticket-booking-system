using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;
using AirportTicketBookingSystem.Application.Interfaces.Service;
using AirportTicketBookingSystem.Domain.Interfaces;

namespace AirportTicketBookingSystem.Application.Service;

public class ReflectionService : IReflectionService
{
    public const string EntitiesNamespace = "AirportTicketBookingSystem.Domain";

    public string ReportPropertiesWithAttributes(Type type)
    {
        var sb = new StringBuilder();
        sb.AppendLine($"Properties and Attributes for model: {type.Name}");
        sb.AppendLine(new string('-', 40));

        foreach (var property in type.GetProperties())
        {
            sb.AppendLine($"Property: {property.Name}");
            sb.AppendLine($"Type: {property.PropertyType.Name}");

            var attributes = property.GetCustomAttributes();
            foreach (var attribute in attributes)
            {
                if (attribute is RequiredAttribute)
                {
                    sb.AppendLine(" - Attribute: Required");
                }
                else
                {
                    sb.AppendLine($" - Attribute: {attribute.GetType().Name}");
                }
            }

            sb.AppendLine(new string('-', 40));
        }

        return sb.ToString();
    }

    public IEnumerable<Type> GetClassTypesInNamespace(string namespaceFilter)
    {
        return Assembly
            .GetExecutingAssembly()
            .GetTypes()
            .Where(type => type is { IsClass: true, IsAbstract: false, Namespace: not null } &&
                           type.Namespace.StartsWith(namespaceFilter, StringComparison.Ordinal));
    }

    public IEnumerable<Type> GetDomainEntityTypes()
    {
        return GetClassTypesInNamespace(EntitiesNamespace)
            .Where(type => typeof(IEntity).IsAssignableFrom(type));
    }
}