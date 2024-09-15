namespace ModularMonolith.Shared.Events
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface IEventPublisher
    {
        Task Publish(IIntegrationEvent integrationEvent, string moduleName, CancellationToken cancellationToken);
    }
}
