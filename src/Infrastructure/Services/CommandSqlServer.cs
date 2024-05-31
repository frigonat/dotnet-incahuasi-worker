using Andreani.Arq.Cqrs.Command;
using Andreani.Arq.Cqrs.Interfaces;
using dotnet_incahuasi_worker.Application.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace dotnet_incahuasi_worker.Infrastructure.Services
{
    public class CommandSqlServer([FromKeyedServices("default")] ITransactionalConfiguration config) : TransactionalRepository(config), ICommandSqlServer
    {
    }
}
