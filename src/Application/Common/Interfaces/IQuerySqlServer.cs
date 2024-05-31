using Andreani.Arq.Core.Interface;
using dotnet_incahuasi_worker.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace dotnet_incahuasi_worker.Application.Common.Interfaces
{
    public interface IQuerySqlServer: IReadOnlyQuery
    {
        public Task<Person> GetPersonByNameAsync(string name);

        public Task<Pedido> GetPedidoByIdAsync(Guid id);
    }
}
