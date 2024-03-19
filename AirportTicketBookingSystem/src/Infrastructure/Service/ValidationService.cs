using System.ComponentModel.DataAnnotations;
using AirportTicketBookingSystem.Domain.Interfaces;
using AirportTicketBookingSystem.Domain.Interfaces.Service;

namespace AirportTicketBookingSystem.Infrastructure.Service;

public class ValidationService : IValidationService
{
    public TEntity ValidateEntityOrThrow<TEntity>(TEntity entity) where TEntity : IEntity
    {
        var context = new ValidationContext(entity, serviceProvider: null, items: null);
        var validationResults = new List<ValidationResult>();

        if (!Validator.TryValidateObject(entity, context, validationResults, true))
            throw new ValidationException(
                $"Validation failed for {typeof(TEntity).Name}: {string.Join(", ", validationResults.Select(v => v.ErrorMessage))}");
        return entity;
    }
}