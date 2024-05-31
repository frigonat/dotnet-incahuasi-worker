using Andreani.Arq.AMQStreams.Extensions;
using Andreani.Arq.Cqrs.Extension;
using Andreani.Scheme.Onboarding;
using dotnet_incahuasi_worker.Application.Common.Interfaces;
using dotnet_incahuasi_worker.Infrastructure.EventHandler;
using dotnet_incahuasi_worker.Infrastructure.Persistence;
using dotnet_incahuasi_worker.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace dotnet_incahuasi_worker.Infrastructure.Boopstrap;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCQRS(configuration)
        .Configure<ApplicationDbContext>();

        services.AddKafka(configuration)
           .ToConsumer<Suscriber, Pedido>("OnboardingBackendFernando-Andreani.Scheme.Onboarding.PedidoAsignado")
           .Build();

        services.AddScoped<ICommandSqlServer, CommandSqlServer>();
        services.AddScoped<IQuerySqlServer, QuerySqlServer>();

        return services;
    }
}