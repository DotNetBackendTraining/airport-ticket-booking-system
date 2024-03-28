using AirportTicketBookingSystem.Infrastructure.Interfaces;
using AirportTicketBookingSystem.Infrastructure.Service.Database;
using AirportTicketBookingSystem.Test.Common;
using AirportTicketBookingSystem.Test.Domain;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using Xunit;

namespace AirportTicketBookingSystem.Test.Infrastructure.Service.Database;

public class DatabasePersistenceServiceTests
{
    [Theory, AutoMoqData]
    public void Exists_ShouldReturnTrue_IfAndOnlyIfEntityExists(
        List<Entity> existingEntities,
        Entity newEntity,
        [Frozen] Mock<IFileService<Entity>> fileServiceMock,
        DatabasePersistenceService<Entity> persistenceService)
    {
        var existingEntity = existingEntities.First();
        fileServiceMock.Setup(s => s.ReadAll()).Returns(existingEntities);

        persistenceService.Exists(existingEntity)
            .Should().BeTrue();

        persistenceService.Exists(newEntity)
            .Should().BeFalse();
    }

    [Theory, AutoMoqData]
    public void GetAll_ShouldCallReadAll(
        List<Entity> entities,
        [Frozen] Mock<IFileService<Entity>> fileServiceMock,
        DatabasePersistenceService<Entity> persistenceService)
    {
        fileServiceMock.Setup(s => s.ReadAll()).Returns(entities);

        var result = persistenceService.GetAll();

        result.Should().BeSameAs(entities);
        fileServiceMock.Verify(s => s.ReadAll(), Times.Once);
    }

    [Theory, AutoMoqData]
    public async Task Add_ShouldCallAppendAllAsync_WithEntity(
        Entity entity,
        [Frozen] Mock<IFileService<Entity>> fileServiceMock,
        DatabasePersistenceService<Entity> persistenceService)
    {
        await persistenceService.Add(entity);
        fileServiceMock.Verify(s => s.AppendAllAsync(It.Is<IEnumerable<Entity>>(e => e.Contains(entity))), Times.Once);
    }

    [Theory, AutoMoqData]
    public async Task Update_ShouldReplaceExistingEntityAndCallWriteAllAsync(
        List<Entity> entities,
        Entity newEntity,
        [Frozen] Mock<IFileService<Entity>> fileServiceMock,
        DatabasePersistenceService<Entity> persistenceService)
    {
        var entityToUpdate = entities.First();
        newEntity.Id = entityToUpdate.Id;
        fileServiceMock
            .Setup(s => s.ReadAll())
            .Returns(entities.ToList());

        await persistenceService.Update(newEntity);

        fileServiceMock.Verify(s => s.WriteAllAsync(It.Is<IEnumerable<Entity>>(e =>
                e.Contains(newEntity) && !e.Any(e => e.Id == entityToUpdate.Id && !e.Equals(newEntity)))),
            Times.Once);
    }

    [Theory, AutoMoqData]
    public async Task Delete_ShouldCallWriteAllAsync_WithoutDeletedEntity(
        List<Entity> entities,
        [Frozen] Mock<IFileService<Entity>> fileServiceMock,
        DatabasePersistenceService<Entity> persistenceService)
    {
        var entityToDelete = entities.First();
        fileServiceMock.Setup(s
            => s.ReadAll()).Returns(entities);

        await persistenceService.Delete(entityToDelete);

        fileServiceMock
            .Verify(s => s.WriteAllAsync(It.Is<IEnumerable<Entity>>(e => !e.Contains(entityToDelete))),
                Times.Once);
    }
}