namespace alexn.Eventbus
{
    public interface IEventDispatcher
    {
        void Dispatch<TEvent>(TEvent @event) where TEvent : DomainEvent;
    }
}