using AirportTicketBookingSystem.Infrastructure.Interfaces;
using AirportTicketBookingSystem.Infrastructure.Service.Database;
using AirportTicketBookingSystem.Test.Common;
using AirportTicketBookingSystem.Test.Domain;
using AutoFixture.Xunit2;
using Moq;
using Xunit;

namespace AirportTicketBookingSystem.Test.Infrastructure.Service.Database;

public class DatabaseValidationLayerTests
{
    [Theory, AutoMoqData]
    public async Task AddAsync_ShouldValidateBeforeCall(
        Entity entity,
        [Frozen] Mock<IValidationService> validationServiceMock,
        [Frozen] Mock<ICrudDatabaseService<Entity>> crudServiceMock,
        DatabaseValidationLayer<Entity> validationLayer)
    {
        await validationLayer.AddAsync(entity);

        validationServiceMock.Verify(s => s.ValidateEntityOrThrow(entity), Times.Once);
        crudServiceMock.Verify(s => s.AddAsync(entity), Times.Once);
    }

    [Theory, AutoMoqData]
    public async Task UpdateAsync_ShouldValidateBeforeCall(
        Entity entity,
        [Frozen] Mock<IValidationService> validationServiceMock,
        [Frozen] Mock<ICrudDatabaseService<Entity>> crudServiceMock,
        DatabaseValidationLayer<Entity> validationLayer)
    {
        await validationLayer.UpdateAsync(entity);

        validationServiceMock.Verify(s => s.ValidateEntityOrThrow(entity), Times.Once);
        crudServiceMock.Verify(s => s.UpdateAsync(entity), Times.Once);
    }

    [Theory, AutoMoqData]
    public async Task DeleteAsync_ShouldJustCall(
        Entity entity,
        [Frozen] Mock<IValidationService> validationServiceMock,
        [Frozen] Mock<ICrudDatabaseService<Entity>> crudServiceMock,
        DatabaseValidationLayer<Entity> validationLayer)
    {
        await validationLayer.DeleteAsync(entity);

        validationServiceMock.Verify(s => s.ValidateEntityOrThrow(entity), Times.Never);
        crudServiceMock.Verify(s => s.DeleteAsync(entity), Times.Once);
    }
}