using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Common;
using AirportTicketBookingSystem.Infrastructure.Interfaces;
using AirportTicketBookingSystem.Infrastructure.Service.Database.RelationalLayers;
using AirportTicketBookingSystem.Test.Common;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using Xunit;

namespace AirportTicketBookingSystem.Test.Infrastructure.Service.Database.RelationalLayers;

public class PassengerRelationalLayerTests
{
    [Theory, AutoMoqData]
    public async Task AddOrUpdate_ShouldNeverThrow(
        Passenger passenger,
        PassengerRelationalLayer relationalLayer)
    {
        var addAction = () => relationalLayer.Add(passenger);
        var updateAction = () => relationalLayer.Update(passenger);

        await addAction.Should().NotThrowAsync();
        await updateAction.Should().NotThrowAsync();
    }

    [Theory, AutoMoqData]
    public async Task Delete_ShouldThrowIfRelatedBookingExist(
        [Frozen] int fixedBookingId,
        Booking booking,
        Passenger passenger,
        [Frozen] Mock<IQueryDatabaseService<Booking>> bookingQueryServiceMock,
        PassengerRelationalLayer relationalLayer)
    {
        bookingQueryServiceMock
            .Setup(s => s.GetAll())
            .Returns([booking]);

        var action = () => relationalLayer.Delete(passenger);

        await action.Should().ThrowAsync<DatabaseRelationalException>();
    }
}