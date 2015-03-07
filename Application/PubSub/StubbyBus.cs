using System;
using RefactoringDemo.ApplicationEvents;

namespace RefactoringDemo.PubSub
{
    public class StubbyBus : IBus
    {
        public void Publish<T>(T @event) where T : ApplicationEventBase
        {
            Console.WriteLine("{0} published.  Id: {1}. Timestamp: {2}", typeof(T).Name, @event.Id, @event.Timestamp);
            Console.WriteLine();
        }
    }
}
