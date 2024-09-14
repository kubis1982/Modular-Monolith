namespace ModularMonolith.Shared.Messaging
{
    using System;

    /// <summary>
    /// Represents a subscriber for a specific event type in a module.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="Subscriber"/> class.
    /// </remarks>
    /// <param name="moduleName">The name of the module.</param>
    /// <param name="eventType">The type of the event.</param>
    public class Subscriber(string moduleName, Type eventType)
    {
        /// <summary>
        /// Gets the name of the module.
        /// </summary>
        public string ModuleName { get; } = moduleName;

        /// <summary>
        /// Gets the type of the event.
        /// </summary>
        public Type EventType { get; } = eventType;
    }
}
