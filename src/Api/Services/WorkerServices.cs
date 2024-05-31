using dotnet_incahuasi_worker.Application.UseCase.V1.PersonOperation.Queries.GetList;
using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Services
{
  public class WorkerServices : BackgroundService
  {
    private readonly IServiceScopeFactory _scopeFactory;

    public WorkerServices(IServiceScopeFactory scopeFactory)
    {
      _scopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory));
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {

      using (var scope = _scopeFactory.CreateScope())
      {
        var scopedService = scope.ServiceProvider.GetRequiredService<IScopedService>();
        await scopedService.DoWork();
      }

    }
  }

  public interface IScopedService
  {
    Task DoWork();
  }

  public class ScopedService : IScopedService
  {
    private readonly ILogger<ScopedService> _logger;
    private readonly ISender _mediator;

    public ScopedService(ILogger<ScopedService> logger, ISender mediator)
    {
      _logger = logger;
      _mediator = mediator;
    }

    public async Task DoWork()
    {
      _logger.LogInformation("Ejemplo de BackgroundServices");
      await _mediator.Send(new ListPerson() { });
    }
  }
}
