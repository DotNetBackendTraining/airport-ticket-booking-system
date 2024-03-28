using System.ComponentModel.DataAnnotations;
using AirportTicketBookingSystem.Domain.Interfaces;

namespace AirportTicketBookingSystem.Infrastructure.Interfaces;

public interface IValidationService
{
    /// <summary>
    /// Validates entity's properties against its defined validation attributes.
    /// </summary>
    /// <param name="entity">The entity to validate.</param>
    /// <returns>Reference to the same (validated) entity.</returns>
    /// <exception cref="ValidationException">Thrown when the entity fails to satisfy its validation attributes.</exception>
    public TEntity ValidateEntityOrThrow<TEntity>(TEntity entity)
        where TEntity : IEntity;
}