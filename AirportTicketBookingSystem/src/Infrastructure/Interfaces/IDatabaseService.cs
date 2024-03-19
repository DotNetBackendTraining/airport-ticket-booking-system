using System.ComponentModel.DataAnnotations;
using AirportTicketBookingSystem.Domain.Interfaces;

namespace AirportTicketBookingSystem.Infrastructure.Interfaces;

/// <summary>
/// Defines the basic database methods for managing entities of type <typeparamref name="TEntity"/>.
/// </summary>
/// <typeparam name="TEntity">The type of the entity this service is responsible for.</typeparam>
public interface IDatabaseService<TEntity>
    where TEntity : IEntity
{
    public IEnumerable<TEntity> GetAll();

    /// <summary>
    /// Adds a single entity of type <typeparamref name="TEntity"/> to the database.
    /// </summary>
    /// <param name="entity">The entity to be added.</param>
    /// <returns>A task representing the add operation.</returns>
    /// <exception cref="ArgumentException">Thrown when an entity already exists in the database.</exception>
    /// <exception cref="ValidationException">Thrown when an entity is invalid (against its own attributes).</exception>
    public Task Add(TEntity entity);

    /// <summary>
    /// Updates an existing entity with a new entity in the database.
    /// Finds the old entity by comparing the new entity with `Object.Equals()` method.
    /// </summary>
    /// <param name="newEntity">The new entity to replace the old entity.</param>
    /// <exception cref="KeyNotFoundException">Thrown when the old entity does not exist.</exception>
    /// <exception cref="ValidationException">Thrown when an entity is invalid (against its own attributes).</exception>
    /// <returns>A task representing the update operation.</returns>
    /// <remarks>
    /// Before updating an entity, ensure that all dependent entities have been appropriately managed to avoid violating relational constraints.
    /// </remarks>
    public Task Update(TEntity newEntity);

    /// <summary>
    /// Deletes an entity of type <typeparamref name="TEntity"/> from the database.
    /// </summary>
    /// <param name="entity">The entity to be removed.</param>
    /// <exception cref="KeyNotFoundException">Thrown when the entity does not exist.</exception>
    /// <returns>A task representing the delete operation.</returns>
    /// <remarks>
    /// Before deleting an entity, ensure that all dependent entities have been appropriately managed to avoid violating relational constraints.
    /// </remarks>
    public Task Delete(TEntity entity);

    [Obsolete("Method not implemented yet")]
    public IEnumerable<Task> BatchAdd(IEnumerable<TEntity> entities) => [];
}