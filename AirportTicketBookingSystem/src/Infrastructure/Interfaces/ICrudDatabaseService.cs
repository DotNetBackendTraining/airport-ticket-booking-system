using AirportTicketBookingSystem.Domain.Common;
using AirportTicketBookingSystem.Domain.Interfaces;

namespace AirportTicketBookingSystem.Infrastructure.Interfaces;

/// <summary>
/// Defines the basic CRUD methods for managing entities of type <typeparamref name="TEntity"/>.
/// </summary>
/// <typeparam name="TEntity">Type of domain entity.</typeparam>
public interface ICrudDatabaseService<in TEntity>
    where TEntity : IEntity
{
    /// <summary>
    /// Adds a single entity of type <typeparamref name="TEntity"/> to the database.
    /// </summary>
    /// <param name="entity">The entity to be added.</param>
    /// <returns>A task representing the add operation.</returns>
    /// <exception cref="DatabaseOperationException">Thrown when an entity already exists in the database.
    /// Or when entity is invalid (against its own attributes).</exception>
    /// <exception cref="DatabaseRelationalException">Thrown when a violation of relational constraints occurs.</exception>
    Task Add(TEntity entity);

    /// <summary>
    /// Updates an existing entity with a new entity in the database.
    /// Finds the old entity by comparing the new entity with `Object.Equals()` method.
    /// </summary>
    /// <param name="newEntity">The new entity to replace the old entity.</param>
    /// <exception cref="DatabaseOperationException">Thrown when the old entity does not exist.
    /// Or when entity is invalid (against its own attributes).</exception>
    /// <exception cref="DatabaseRelationalException">Thrown when a violation of relational constraints occurs.</exception>
    /// <returns>A task representing the update operation.</returns>
    Task Update(TEntity newEntity);

    /// <summary>
    /// Deletes an entity of type <typeparamref name="TEntity"/> from the database.
    /// </summary>
    /// <param name="entity">The entity to be removed.</param>
    /// <exception cref="DatabaseOperationException">Thrown when the entity does not exist.</exception>
    /// <exception cref="DatabaseRelationalException">Thrown when a violation of relational constraints occurs.</exception>
    /// <returns>A task representing the delete operation.</returns>
    Task Delete(TEntity entity);
}