using Andreani.Arq.Core.Pipeline.Extension;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace dotnet_incahuasi_worker.Application.Boopstrap;

public static class DependencyInjection
{
  public static IServiceCollection AddApplication(this IServiceCollection services)
  {
    services.AddAutoMapper(Assembly.GetExecutingAssembly());
    services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    services.AddMediatR(cfg =>
    {
      cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
    });
    services.AddAndreaniPipeline(Verbose: true);

    return services;
  }
}
