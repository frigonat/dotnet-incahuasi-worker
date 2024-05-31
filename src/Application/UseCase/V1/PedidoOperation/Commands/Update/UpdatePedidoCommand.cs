using Andreani.Arq.Pipeline.Clases;
using dotnet_incahuasi_worker.Application.Common.Interfaces;
using dotnet_incahuasi_worker.Application.UseCase.V1.PersonOperation.Commands.Update;
using dotnet_incahuasi_worker.Domain.Common;
using dotnet_incahuasi_worker.Domain.Entities;
using dotnet_incahuasi_worker.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace dotnet_incahuasi_worker.Application.UseCase.V1.PedidoOperation.Commands.Update
{
    public class UpdatePedidoCommand : IRequest<Response<string>>
    {
        public required string id { get; set; }
        public required int numeroDePedido { get; set; }    
    }

    public class UpdatePedidoHandler(ICommandSqlServer repository, IQuerySqlServer query, ILogger<UpdatePedidoHandler> logger) : IRequestHandler<UpdatePedidoCommand, Response<string>>
    {
        public async Task<Response<string>> Handle(UpdatePedidoCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<string>();
            var pedido = new Pedido();

            if (Guid.TryParse(request.id, out Guid resultado))
            {
                try
                {
                    pedido = await query.GetPedidoByIdAsync(resultado);
                }
                catch (Exception ex)
                {
                    pedido = null;
                    Console.WriteLine("Error:" + ex.Message);
                }

                if (pedido is null)
                {
                    response.AddNotification("#3123", "Pedido", $"No se ha podido encontrar un pedido con el id recibido: {request.id}.-");
                    response.StatusCode = System.Net.HttpStatusCode.NotFound;
                    return response;
                }
                pedido.numeroDePedido = request.numeroDePedido;
                pedido.estadoDelPedido = (int)EstadosDePedidos.Asignado;

                repository.Update(pedido);
                await repository.SaveChangeAsync();

                logger.LogInformation($"Se actualizó el pedido con id {pedido.id} al que se le colocó el número {pedido.numeroDePedido}.");

                return response;
            }
            else
            {
                response.AddNotification("#6031-ii", nameof(request.id), string.Format(ErrorMessage.ID_INVALIDO, nameof(Pedido), request.id));
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                return response;
            }
        }
    }
}
