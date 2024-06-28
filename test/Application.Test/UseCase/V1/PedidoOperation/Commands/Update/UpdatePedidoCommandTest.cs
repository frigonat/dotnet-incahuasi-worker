using AutoFixture;
using dotnet_incahuasi_worker.Application.Common.Interfaces;
using dotnet_incahuasi_worker.Application.UseCase.V1.PedidoOperation.Commands.Update;
using dotnet_incahuasi_worker.Application.UseCase.V1.PersonOperation.Commands.Create;
using dotnet_incahuasi_worker.Domain.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Test.UseCase.V1.PedidoOperation.Commands.Update
{
    public class UpdatePedidoCommandTest
    {
        private readonly Mock<ICommandSqlServer> _repository;
        private readonly Mock<IQuerySqlServer> _query;
        private readonly Mock<ILogger<UpdatePedidoHandler>>_logger;
        private readonly UpdatePedidoHandler _handler;
        private CancellationToken _cancellationToken;
        

        public UpdatePedidoCommandTest()
        {
            _repository = new();
            _query = new();
            _logger = new Mock<ILogger<UpdatePedidoHandler>>();
            _cancellationToken = CancellationToken.None;
            
            _handler = new UpdatePedidoHandler(_repository.Object, _query.Object, _logger.Object);
        }

        [Fact]
        public async Task Handle_Update_Pedido_Success()
        {
            // Arrange
            var request = new Fixture().Create<UpdatePedidoCommand>();
            var unPedido = new Fixture().Create<Pedido>();
            request.id = new Fixture().Create<Guid>().ToString();   

            _query.Setup(_ => _.GetPedidoByIdAsync(It.IsAny<Guid>())).ReturnsAsync(unPedido);   //.ThrowsAsync(new DbUpdateException());

            // Act
            var result = await _handler.Handle(request, _cancellationToken);

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Handle_Update_Pedido_Error_IdNoValido()
        {
            // Arrange
            var request = new Fixture().Create<UpdatePedidoCommand>();

            // Act
            var result = await _handler.Handle(request, _cancellationToken);

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Handle_Update_Pedido_Error_DbException()
        {
            // Arrange
            var request = new Fixture().Create<UpdatePedidoCommand>();
            var unPedido = new Fixture().Create<Pedido>();
            request.id = new Fixture().Create<Guid>().ToString();

            _query.Setup(_ => _.GetPedidoByIdAsync(It.IsAny<Guid>())).ThrowsAsync(new DbUpdateException());

            // Act
            var result = await _handler.Handle(request, _cancellationToken);

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

    }
}
