using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Core.Logging;
using Moq;
using RefactoringDemo.ApplicationEvents;
using RefactoringDemo.PubSub;
using RefactoringDemo.Storage;
using TechTalk.SpecFlow;

namespace RefactoringDemo.Specs
{
    public class ReminderCreatorHelper
    {
        private readonly IBus bus;
        private readonly IPatientStorage patientStorage;
        private readonly IAppointmentStorage appointmentStorage;
        private readonly ILogger logger;

        public ReminderCreatorHelper()
        {
            SentSmsRequests = new List<SendSmsRequested>();
            LoggedMessages = new List<string>();
            PublishedEvents = new List<ApplicationEventBase>();
            this.logger = Mock.Of<ILogger>();
            this.bus = Mock.Of<IBus>();
            this.patientStorage = Mock.Of<IPatientStorage>();
            this.appointmentStorage = Mock.Of<IAppointmentStorage>();
        }

        public List<SendSmsRequested> SentSmsRequests { get; private set; }
        public List<string> LoggedMessages { get; private set; }
        public List<ApplicationEventBase> PublishedEvents { get; private set; } 

        public ReminderCreator SetupReminderCreator()
        {
            var messageCreator= new StubbyMessageCreator();
            var reminderCreator = new ReminderCreator(appointmentStorage, patientStorage, messageCreator, bus);
            reminderCreator.Logger = logger;
            SetUpBus();
            SetUpPatientStorage();
            SetUpAppointmentStorage();
            SetUpLogger();
            return reminderCreator;
        }

        private void SetUpLogger()
        {
            Mock.Get(logger).Setup(x => x.DebugFormat(It.IsAny<string>(), It.IsAny<object[]>()))
                .Callback((string x, object[] y) => LoggedMessages.Add(string.Format(x, y)));
        }

        private void SetUpBus()
        {
            Mock.Get(bus)
                .Setup(x => x.Publish(It.IsAny<ApplicationEventBase>()))
                .Callback((ApplicationEventBase x) => PublishedEvents.Add(x));

            Mock.Get(bus)
               .Setup(x => x.Publish(It.IsAny<SendSmsRequested>()))
               .Callback((SendSmsRequested x) => SentSmsRequests.Add(x));  
        }

        private void SetUpPatientStorage()
        {
            Guid patientIdParameter = Guid.Empty;
            Mock.Get(patientStorage).Setup(x => x.GetPatientContacts(It.IsAny<Guid>()))
               .Callback((Guid x) => patientIdParameter = x)
               .Returns(patientContactsFromFullList(patientIdParameter));
        }

        private void SetUpAppointmentStorage()
        {
            Mock.Get(appointmentStorage).Setup(x => x.GetAppointmentsRequiringReminders())
                .Returns(ScenarioContext.Current.Get<List<Appointment>>(ScenarioContextKeys.Appointments));
        }
        private readonly Func<Guid, IEnumerable<PatientContact>> patientContactsFromFullList =
            x => ScenarioContext.Current.Get<IEnumerable<PatientContact>>(ScenarioContextKeys.PatientContacts)
                .Where(y => y.PatientId == x);
    }
}