namespace ModularMonolith.Shared.Events
{
    using ModularMonolith.Shared.CQRS.Commands;
    using ModularMonolith.Shared.Extensions;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed class EventPublisher(ICommandExecutor commandExecutor) : IEventPublisher
    {
        public async Task Publish(IIntegrationEvent integrationEvent, string moduleName, CancellationToken cancellationToken)
        {
            var eventCommandType = typeof(EventCommand<>).MakeGenericType(integrationEvent.GetType());
            Type? type = GetType(eventCommandType, moduleName);
            if (type != null)
            {
                Command command = (Command)Activator.CreateInstance(type, integrationEvent)!;
                await commandExecutor.Execute(command, cancellationToken);
            }
        }

        private static Type? GetType(Type handlerType, string moduleName)
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetModuleAssemblies(moduleName))
            {
                foreach (var type in assembly.GetTypes().Where(n => n.IsSubclassOf(handlerType)))
                {
                    return type;
                }
            }
            return null;
        }
    }
}
