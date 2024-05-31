using Andreani.Arq.AMQStreams.Class;
using Andreani.Arq.AMQStreams.Interface;
using Andreani.Scheme.Onboarding;
using dotnet_incahuasi_worker.Application.UseCase.V1.PedidoOperation.Commands.Update;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace dotnet_incahuasi_worker.Infrastructure.EventHandler
{
    public class Suscriber : BackgroundService, ISubscriber
    {
        private readonly IConfiguration _configuration;
        private readonly ISender _mediator;
        private readonly ILogger<Suscriber> _logger;

        public Suscriber(ILogger<Suscriber> logger, ISender mediator, IConfiguration configuration)
        {
            _configuration = configuration;
            _mediator = mediator;
            _logger = logger;
        }
        public async Task ReceiveCustomEvent(Pedido @event, ConsumerMetadata metaData)
        {
            _logger.LogInformation("Se ha recibido el evento con {id}.", @event.id);

            Console.WriteLine($"ID: {@event.id}");
            Console.WriteLine($"Número de Pedido: {@event.numeroDePedido}");

            await _mediator.Send(new UpdatePedidoCommand() { id = @event.id, numeroDePedido = @event.numeroDePedido});

            _logger.LogInformation($"Se finalizó el proceso del evento {@event.id}.-");
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            throw new NotImplementedException();
        }
    }
}

