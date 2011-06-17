using System;
using System.Linq;
using System.Reflection;

namespace alexn.Eventbus {
    public class StaticEventDispatcher : IEventDispatcher {
        private readonly Assembly[] _assemblies;

        public StaticEventDispatcher(params Assembly[] assemblies) {
            _assemblies = assemblies;
        }

        public void Dispatch<TEvent>(TEvent @event) where TEvent : DomainEvent {
            foreach(var assembly in _assemblies) {
                var handlers = assembly.GetTypes().Where(x => typeof(IHandle<TEvent>).IsAssignableFrom(x));

                foreach (var handler in handlers) {
                    var instance = Activator.CreateInstance(handler) as IHandle<TEvent>;
                    if (instance != null)
                        instance.Handle(@event);
                }
            }
        }
    }
}