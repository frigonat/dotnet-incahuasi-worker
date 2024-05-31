using Andreani.Arq.Cqrs.Interfaces;
using Andreani.Arq.Cqrs.Queries;
using dotnet_incahuasi_worker.Application.Common.Interfaces;
using dotnet_incahuasi_worker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using dotnet_incahuasi_worker.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace dotnet_incahuasi_worker.Infrastructure.Services
{
    public class QuerySqlServer : ReadOnlyQuery, IQuerySqlServer
    {
        private readonly ApplicationDbContext _context;
        public QuerySqlServer([FromKeyedServices("default")] IReadOnlyQueryConfiguration config,
            [FromKeyedServices("default")] ApplicationDbContext context) : base(config)
        {
          _context = context;
        }

        public async Task<Person> GetPersonByNameAsync(string name)
        {
          return await _context.Person.FirstAsync(x => x.Nombre == name);
        }

        public async Task<Pedido> GetPedidoByIdAsync(Guid id)
        {
            Pedido p; 
            try
            {
                p = await _context.Pedido.FirstAsync(x => x.id == id);
            }
            catch (Exception ex)
            {
                p = null;
                string a = ex.Message;
            }

            return p;    
        }
    }
}
