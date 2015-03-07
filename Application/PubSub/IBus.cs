using RefactoringDemo.ApplicationEvents;

namespace RefactoringDemo.PubSub
{
    public interface IBus
    {
        void Publish<T>(T @event) where T: ApplicationEventBase;
    }
}
