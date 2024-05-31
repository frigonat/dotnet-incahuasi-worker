using AutoFixture;
using dotnet_incahuasi_worker.Application.Common.Interfaces;
using dotnet_incahuasi_worker.Application.UseCase.V1.PersonOperation.Queries.GetByName;
using dotnet_incahuasi_worker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Net;
using FluentAssertions;

namespace Application.Test.UseCase.V1.PersonOperation.Queries.GetByName
{
    public class GetPersonByNameTest
    {
        private readonly Mock<IQuerySqlServer> _query;
        private readonly GetPersonByNameHandler _handler;
        private CancellationToken _cancellationToken;

        public GetPersonByNameTest() 
        {
            _query = new();
            _handler = new(_query.Object);
            _cancellationToken = CancellationToken.None;
        }

        [Fact]
        public async Task Handler_GetPersonByName_Success()
        {
            // Arrange
            var request = new Fixture().Create<GetPersonByName>();
            var person = new Fixture().Create<Person>();
            _query.Setup(_ => _.GetPersonByNameAsync(It.IsAny<string>())).ReturnsAsync(person);

            // Act
            var result = await _handler.Handle(request, _cancellationToken);

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Content.Should().NotBeNull();
            Assert.Equal(person, result.Content);
        }

        [Fact]
        public async Task Handler_GetPersonByName_PersonNotExist()
        {
            // Arrange
            var request = new Fixture().Create<GetPersonByName>();
            _query.Setup(_ => _.GetPersonByNameAsync(It.IsAny<string>()))
                .ReturnsAsync((Person)null);

            // Act
            var result = await _handler.Handle(request, _cancellationToken);

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public async Task Handler_GetPersonByName_ThrowUpdateDatabase()
        {
            // Arrange
            var request = new Fixture().Create<GetPersonByName>();
            _query.Setup(_ => _.GetPersonByNameAsync(It.IsAny<string>()))
                .ThrowsAsync(new DbUpdateException());

            // Act
            // Assert
            await Assert.ThrowsAsync<DbUpdateException>(() => _handler.Handle(request, _cancellationToken));

        }
    }
}
