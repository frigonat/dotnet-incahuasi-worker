using dotnet_incahuasi_worker.Domain.Common;
using System.Threading.Tasks;

namespace dotnet_incahuasi_worker.Application.Common.Interfaces;

public interface IDomainEventService
{
    Task Publish(DomainEvent domainEvent);
}
