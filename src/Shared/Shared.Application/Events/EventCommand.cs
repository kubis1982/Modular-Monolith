namespace ModularMonolith.Shared.Events
{
    using ModularMonolith.Shared.CQRS.Commands;

    public abstract record EventCommand<TEvent>(TEvent Event) : Command where TEvent : class, IIntegrationEvent
    {
    }

    public abstract class EventCommandHandler<TEventCommand> : CommandHandler<TEventCommand> where TEventCommand : Command
    {
    }
}
