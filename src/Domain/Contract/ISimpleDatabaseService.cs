namespace AirportTicketBookingSystem.Domain.Contract;

/// <summary>
/// Represents a simplified database service for managing entities of type <typeparamref name="TEntity"/>.
/// This service provides basic CRUD operations with limited constraint checking.
/// </summary>
/// <remarks>
/// Use this service with caution, as it only enforces simple database constraints such as uniqueness and existence.
/// It does not manage or enforce relational constraints or dependencies between entities.
/// For example, when deleting an entity that might have dependent entities in the database (e.g., deleting a flight without handling its associated bookings),
/// you must manually ensure that all related entities are appropriately handled to maintain database integrity.
/// </remarks>
/// <typeparam name="TEntity">The type of the entity this service is responsible for.</typeparam>
public interface ISimpleDatabaseService<TEntity>
{
    public IEnumerable<TEntity> GetAll();

    /// <summary>
    /// Adds a single entity of type <typeparamref name="TEntity"/> to the database.
    /// </summary>
    /// <param name="entity">The entity to be added.</param>
    /// <exception cref="ArgumentException">Thrown when an entity already exists in the database.</exception>
    public void Add(TEntity entity);

    /// <summary>
    /// Updates an existing entity with a new entity in the database.
    /// Finds the old entity by comparing the new entity with `Object.Equals()` method.
    /// </summary>
    /// <param name="newEntity">The new entity to replace the old entity.</param>
    /// <exception cref="KeyNotFoundException">Thrown when the old entity does not exist.</exception>
    /// <remarks>
    /// Before updating an entity, ensure that all dependent entities have been appropriately managed to avoid violating relational constraints.
    /// </remarks>
    public void Update(TEntity newEntity);

    /// <summary>
    /// Deletes an entity of type <typeparamref name="TEntity"/> from the database.
    /// </summary>
    /// <param name="entity">The entity to be removed.</param>
    /// <returns>A boolean to indicate whether the entity was found and deleted.</returns>
    /// <remarks>
    /// Before deleting an entity, ensure that all dependent entities have been appropriately managed to avoid violating relational constraints.
    /// </remarks>
    public bool Delete(TEntity entity);

    [Obsolete("Method not implemented yet")]
    public IEnumerable<Task> BatchAdd(IEnumerable<TEntity> entities) => [];
}