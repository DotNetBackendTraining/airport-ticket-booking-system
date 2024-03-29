using AirportTicketBookingSystem.Domain.Common;
using AirportTicketBookingSystem.Infrastructure.Interfaces;
using AirportTicketBookingSystem.Infrastructure.Service.Database;
using AirportTicketBookingSystem.Test.Common;
using AirportTicketBookingSystem.Test.Domain;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using Xunit;

namespace AirportTicketBookingSystem.Test.Infrastructure.Service.Database;

public class DatabaseUniquenessLayerTests
{
    [Theory, AutoMoqData]
    public async Task Add_EntityExists_ShouldThrow(
        Entity entity,
        [Frozen] Mock<IQueryDatabaseService<Entity>> queryServiceMock,
        DatabaseUniquenessLayer<Entity> uniquenessLayer)
    {
        queryServiceMock
            .Setup(s => s.Exists(entity))
            .Returns(true);

        var action = () => uniquenessLayer.Add(entity);

        await action.Should().ThrowAsync<DatabaseOperationException>();
    }

    [Theory, AutoMoqData]
    public async Task Add_EntityDoesNotExist_ShouldCallCrud(
        Entity entity,
        [Frozen] Mock<IQueryDatabaseService<Entity>> queryServiceMock,
        [Frozen] Mock<ICrudDatabaseService<Entity>> crudServiceMock,
        DatabaseUniquenessLayer<Entity> uniquenessLayer)
    {
        queryServiceMock
            .Setup(s => s.Exists(entity))
            .Returns(false);

        var action = () => uniquenessLayer.Add(entity);

        await action.Should().NotThrowAsync();
        crudServiceMock.Verify(s => s.Add(entity), Times.Once);
    }

    [Theory, AutoMoqData]
    public async Task UpdateOrDelete_EntityDoesNotExist_ShouldThrow(
        Entity entity,
        [Frozen] Mock<IQueryDatabaseService<Entity>> queryServiceMock,
        DatabaseUniquenessLayer<Entity> uniquenessLayer)
    {
        queryServiceMock
            .Setup(s => s.Exists(entity))
            .Returns(false);

        var updateAction = () => uniquenessLayer.Update(entity);
        var deleteAction = () => uniquenessLayer.Delete(entity);

        await updateAction.Should().ThrowAsync<DatabaseOperationException>();
        await deleteAction.Should().ThrowAsync<DatabaseOperationException>();
    }

    [Theory, AutoMoqData]
    public async Task UpdateOrDelete_EntityExists_ShouldCallCrud(
        Entity entity,
        [Frozen] Mock<IQueryDatabaseService<Entity>> queryServiceMock,
        [Frozen] Mock<ICrudDatabaseService<Entity>> crudServiceMock,
        DatabaseUniquenessLayer<Entity> uniquenessLayer)
    {
        queryServiceMock
            .Setup(s => s.Exists(entity))
            .Returns(true);

        var updateAction = () => uniquenessLayer.Update(entity);
        var deleteAction = () => uniquenessLayer.Delete(entity);

        await updateAction.Should().NotThrowAsync();
        crudServiceMock.Verify(s => s.Update(entity), Times.Once);
        await deleteAction.Should().NotThrowAsync();
        crudServiceMock.Verify(s => s.Delete(entity), Times.Once);
    }
}