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
    public async Task Add_ShouldValidateBeforeCall(
        Entity entity,
        [Frozen] Mock<IValidationService> validationServiceMock,
        [Frozen] Mock<ICrudDatabaseService<Entity>> crudServiceMock,
        DatabaseValidationLayer<Entity> validationLayer)
    {
        await validationLayer.Add(entity);

        validationServiceMock.Verify(s => s.ValidateEntityOrThrow(entity), Times.Once);
        crudServiceMock.Verify(s => s.Add(entity), Times.Once);
    }

    [Theory, AutoMoqData]
    public async Task Update_ShouldValidateBeforeCall(
        Entity entity,
        [Frozen] Mock<IValidationService> validationServiceMock,
        [Frozen] Mock<ICrudDatabaseService<Entity>> crudServiceMock,
        DatabaseValidationLayer<Entity> validationLayer)
    {
        await validationLayer.Update(entity);

        validationServiceMock.Verify(s => s.ValidateEntityOrThrow(entity), Times.Once);
        crudServiceMock.Verify(s => s.Update(entity), Times.Once);
    }

    [Theory, AutoMoqData]
    public async Task Delete_ShouldJustCall(
        Entity entity,
        [Frozen] Mock<IValidationService> validationServiceMock,
        [Frozen] Mock<ICrudDatabaseService<Entity>> crudServiceMock,
        DatabaseValidationLayer<Entity> validationLayer)
    {
        await validationLayer.Delete(entity);

        validationServiceMock.Verify(s => s.ValidateEntityOrThrow(entity), Times.Never);
        crudServiceMock.Verify(s => s.Delete(entity), Times.Once);
    }
}