using dotnet_incahuasi_worker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace dotnet_incahuasi_worker.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<Person> Person { get; set; }
    
    public DbSet<Pedido> Pedido { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}
