namespace alexn.EventHandling
{
    public interface IEventDispatcher
    {
        void Dispatch<TEvent>(TEvent @event) where TEvent : DomainEvent;
    }
}