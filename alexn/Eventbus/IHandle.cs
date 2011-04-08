namespace alexn.EventHandling
{
    public interface IHandle<in TEvent> where TEvent :class, IDomainEvent
    {
        void Handle(TEvent @event);
    }
}