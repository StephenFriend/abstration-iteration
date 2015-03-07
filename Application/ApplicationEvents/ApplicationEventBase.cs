using System;

namespace RefactoringDemo.ApplicationEvents
{
    public abstract class ApplicationEventBase
    {
        protected ApplicationEventBase()
        {
            Id = Guid.NewGuid();
            Timestamp = DateTime.UtcNow;
        }

        public Guid Id { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
