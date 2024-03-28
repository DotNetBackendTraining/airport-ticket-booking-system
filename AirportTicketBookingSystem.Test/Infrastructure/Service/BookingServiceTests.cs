using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Criteria.Search;
using AirportTicketBookingSystem.Domain.Interfaces.Repository;
using AirportTicketBookingSystem.Domain.Interfaces.Service;
using AirportTicketBookingSystem.Infrastructure.Service;
using AirportTicketBookingSystem.Test.Common;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using Xunit;

namespace AirportTicketBookingSystem.Test.Infrastructure.Service;

public class BookingServiceTests
{
    [Theory, AutoMoqData]
    public void Add_ShouldCallRepository(
        Booking entity,
        [Frozen] Mock<IBookingRepository> repositoryMock,
        BookingService service)
    {
        service.Add(entity);
        repositoryMock.Verify(r => r.Add(entity), Times.Once);
    }

    [Theory, AutoMoqData]
    public void Update_ShouldCallRepository(
        Booking entity,
        [Frozen] Mock<IBookingRepository> repositoryMock,
        BookingService service)
    {
        service.Update(entity);
        repositoryMock.Verify(r => r.Update(entity), Times.Once);
    }

    [Theory, AutoMoqData]
    public void Delete_ShouldCallRepository(
        Booking entity,
        [Frozen] Mock<IBookingRepository> repositoryMock,
        BookingService service)
    {
        service.Delete(entity);
        repositoryMock.Verify(r => r.Delete(entity), Times.Once);
    }

    [Theory, AutoMoqData]
    public void GetById_ShouldCallRepository(
        Booking entity,
        [Frozen] Mock<IBookingRepository> repositoryMock,
        BookingService service)
    {
        repositoryMock
            .Setup(r => r.GetById(entity.FlightId, entity.PassengerId))
            .Returns(entity);

        var returned = service.GetById(entity.FlightId, entity.PassengerId);

        returned.Should().Be(entity);
        repositoryMock.Verify(r => r.GetById(entity.FlightId, entity.PassengerId), Times.Once);
    }

    [Theory, AutoMoqData]
    public void Search_ShouldUseFilterOnAllEntities(
        BookingSearchCriteria criteria,
        List<Booking> allEntities,
        List<Booking> filteredEntities,
        [Frozen] Mock<IBookingRepository> repositoryMock,
        [Frozen] Mock<IFilteringService<Booking, BookingSearchCriteria>> filteringServiceMock,
        BookingService service)
    {
        repositoryMock
            .Setup(r => r.GetAll())
            .Returns(allEntities);
        filteringServiceMock
            .Setup(s => s.Filter(allEntities, criteria))
            .Returns(filteredEntities);

        var returned = service.Search(criteria);

        returned.Should().BeSameAs(filteredEntities);
        repositoryMock.Verify(r => r.GetAll(), Times.Once);
        filteringServiceMock.Verify(s => s.Filter(allEntities, criteria), Times.Once);
    }
}