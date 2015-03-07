using Castle.Facilities.Logging;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using RefactoringDemo.PubSub;
using RefactoringDemo.Storage;

namespace RefactoringDemo
{
    public class Installer : IWindsorInstaller
    {
        public void Install(IWindsorContainer container,IConfigurationStore store)
        {
            container.AddFacility<LoggingFacility>(f => f.UseLog4Net());
            container.Register(
                Component.For<IBus>().ImplementedBy<StubbyBus>(),
                Component.For<IPatientStorage>().ImplementedBy<StubbyPatientStorage>(),
                Component.For<IAppointmentStorage>().ImplementedBy<StubbyAppointmentStorage>(),
                Component.For<IMessageCreator>().ImplementedBy<StubbyMessageCreator>(),
                Component.For<ReminderCreator>()
            );
        }
    }
}
