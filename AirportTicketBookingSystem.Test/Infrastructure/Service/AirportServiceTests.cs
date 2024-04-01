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

public class AirportServiceTests
{
    [Theory, AutoMoqData]
    public async Task AddAsync_ShouldCallRepository(
        Airport entity,
        [Frozen] Mock<IAirportRepository> repositoryMock,
        AirportService service)
    {
        await service.AddAsync(entity);
        repositoryMock.Verify(r => r.AddAsync(entity), Times.Once);
    }

    [Theory, AutoMoqData]
    public void GetById_ShouldCallRepository(
        Airport entity,
        [Frozen] Mock<IAirportRepository> repositoryMock,
        AirportService service)
    {
        repositoryMock
            .Setup(r => r.GetById(entity.Id))
            .Returns(entity);

        var returned = service.GetById(entity.Id);

        returned.Should().Be(entity);
        repositoryMock.Verify(r => r.GetById(entity.Id), Times.Once);
    }

    [Theory, AutoMoqData]
    public void Search_ShouldUseFilterOnAllEntities(
        AirportSearchCriteria criteria,
        List<Airport> allEntities,
        List<Airport> filteredEntities,
        [Frozen] Mock<IAirportRepository> repositoryMock,
        [Frozen] Mock<IFilteringService<Airport, AirportSearchCriteria>> filteringServiceMock,
        AirportService service)
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