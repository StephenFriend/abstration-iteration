using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace RefactoringDemo.Specs
{
    [Binding]
    public class AppointmentReminderSteps
    {
        private readonly PatientHelper patientHelper;
        private readonly ReminderCreatorHelper reminderCreatorHelper;
        private  ReminderCreator reminderCreator;
        private AppointmentHelper appointmentHelper;

        public AppointmentReminderSteps(PatientHelper patientHelper, AppointmentHelper appointmentHelper, ReminderCreatorHelper reminderCreatorHelper)
        {
            this.patientHelper = patientHelper;
            this.reminderCreatorHelper = reminderCreatorHelper;
            this.appointmentHelper = appointmentHelper;
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            ScenarioContext.Current.Set(new List<PatientContact>(), ScenarioContextKeys.PatientContacts);
            ScenarioContext.Current.Set(new List<Appointment>(), ScenarioContextKeys.Appointments);
            reminderCreator = reminderCreatorHelper.SetupReminderCreator();
        }

        [Given(@"there is a patient")]
        public void GivenThereIsAPatient()
        {
            patientHelper.CreateTestPatient();
        }

        [Given(@"they have the following contact details")]
        public void GivenTheyHaveTheFollowingContactDetails(Table tableOfContacts)
        {
            var patientContacts = tableOfContacts.CreateSet<PatientContact>();
            var patientId = ScenarioContext.Current.Get<Guid>(ScenarioContextKeys.PatientOne);
            patientHelper.RegisterContacts(patientId, patientContacts);
        }

        [Given(@"they have a future appointment")]
        public void GivenTheyHaveAFutureAppointment()
        {
            var patientId = ScenarioContext.Current.Get<Guid>(ScenarioContextKeys.PatientOne);
            appointmentHelper.CreateAppointmentForPatient(patientId);
        }


        [When(@"the reminder creator runs")]
        public void WhenTheReminderCreatorRuns()
        {
             reminderCreator.CreateReminders();
        }

        [Then(@"we should send a reminder to each phone number where contact is allowed")]
        public void ThenWeShouldSendAReminderToEachPhoneNumberWhereContactIsAllowed()
        {
            var expectedRecipients = ScenarioContext.Current.Get<List<PatientContact>>(ScenarioContextKeys.PatientContacts)
                            .Where(x => x.IsContactAllowed).Select(x => x.PhoneNumber);
            var actualRecipients = reminderCreatorHelper.SentSmsRequests.Select(x => x.Recipient);
            CollectionAssert.AreEquivalent(expectedRecipients, actualRecipients);
        }
        
        [Then(@"we should log that (.*) '(.*)'")]
        public void ThenWeShouldLogThat(int numberOfItems, string debugMessage)
        {
            Assert.That(reminderCreatorHelper.LoggedMessages.Contains(numberOfItems + " " + debugMessage));
        }

        [Then(@"we should publish (.*) '(.*)'")]
        public void ThenWeShouldPublish(int expectedNumberOfPublishedEvents, string typeOfPublishedEvent)
        {
            var publishedEvents = reminderCreatorHelper.PublishedEvents.Where(x => x.GetType().Name == typeOfPublishedEvent);
            Assert.That(publishedEvents.Count(), Is.EqualTo(expectedNumberOfPublishedEvents));

        }

   
    }
}
