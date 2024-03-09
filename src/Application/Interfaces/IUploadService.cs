using AirportTicketBookingSystem.Application.Result;
using AirportTicketBookingSystem.Domain.Interfaces;

namespace AirportTicketBookingSystem.Application.Interfaces;

/// <summary>
/// Responsible for uploading entities of type <typeparamref name="TEntity"/>.
/// </summary>
/// <typeparam name="TEntity">The type of entity this service is responsible for uploading.</typeparam>
public interface IUploadService<TEntity> where TEntity : IEntity
{
    /// <summary>
    /// Uploads a batch of entities of type <typeparamref name="TEntity"/> from a specified file.
    /// </summary>
    /// <param name="filepath">The path to the file containing entities to be uploaded.</param>
    /// <returns>
    /// An enumerable of <see cref="OperationResult{TEntity}"/>, representing the result of each entity upload operation.
    /// Each result includes the entity, a success flag, and an optional error message if the upload failed.
    /// </returns>
    /// <exception cref="FileNotFoundException">Thrown if the file specified by <paramref name="filepath"/> does not exist.</exception>
    /// <remarks>
    /// This method is designed to process multiple entities in a single operation, improving efficiency over individual uploads.
    /// Entities are read from the file specified by <paramref name="filepath"/>. Each line in the file should represent one entity.
    /// The method attempts to upload all entities in the file, absorbing and reporting individual errors without terminating the operation.
    /// </remarks>
    IEnumerable<OperationResult<TEntity>> BatchUpload(string filepath);
}