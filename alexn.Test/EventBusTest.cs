using System;
using System.Linq;
using System.Reflection;
using alexn.Eventbus;
using NUnit.Framework;

namespace alexn.Test
{
    [TestFixture]
    public class EventBusTest
    {
        [Test]
        public void Can_Handle_Events()
        {
            var dispatcher = new StaticEventDispatcher();
            var called = 0;

            RunCatch<EventCalledException>(() => dispatcher.Dispatch(new AddEvent(1, 1)), () => called++);
            RunCatch<EventCalledException>(() => dispatcher.Dispatch(new AddEvent(1, 1)), () =>called++);

            Assert.AreEqual(2, called);
        }

        public static void RunCatch<TException>(Action action, Action callback) where TException : Exception
        {
            try
            {
                action();
            }
            catch(TException ex)
            {
                callback();
            }
        }
    }

    public class StaticEventDispatcher : IEventDispatcher
    {
        public void Dispatch<TEvent>(TEvent @event) where TEvent : DomainEvent
        {
            var assembly = Assembly.GetExecutingAssembly();
            var handlers = assembly.GetTypes().Where(x => typeof (IHandle<TEvent>).IsAssignableFrom(x));
            
            foreach(var handler in handlers)
            {
                var instance = Activator.CreateInstance(handler) as IHandle<TEvent>;
                instance.Handle(@event);
            }
        }
    }

    public class EventCalledException : Exception
    {
        public EventCalledException(string message) : base(message)
        {
            
        }
    }

    public class AddEventHandler : IHandle<AddEvent>
    {
        public AddEventHandler()
        {
            
        }

        public void Handle(AddEvent @event)
        {
            var sum = @event.X + @event.Y;
            throw new EventCalledException(sum + " is the sum");
        }
    }
    public class AddEvent : DomainEvent
    {
        public int X { get; set; }
        public int Y { get; set; }

        public AddEvent(int x, int y)
        {
            X = x;
            Y = y;
        }
    }


    public class SubtractEvent : DomainEvent
    {
        public int X { get; set; }
        public int Y { get; set; }

        public SubtractEvent(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
    public class SubtractEventHandler :IHandle<SubtractEvent>
    {
        public void Handle(SubtractEvent @event)
        {
            var sum = @event.X - @event.Y;
            throw new EventCalledException(sum + " is the sum");
        }
    }
}