using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Core.Logging;
using RefactoringDemo.ApplicationEvents;
using RefactoringDemo.PubSub;
using RefactoringDemo.Storage;

namespace RefactoringDemo
{
    public class ReminderCreator
    {
        private ILogger logger = NullLogger.Instance;
        private readonly IAppointmentStorage appointmentStorage;
        private readonly IPatientStorage patientStorage;
        private readonly IMessageCreator messageCreator;
        private readonly IBus bus;

        public ReminderCreator(IAppointmentStorage appointmentStorage, IPatientStorage patientStorage, IMessageCreator messageCreator, IBus bus)
        {
            this.appointmentStorage = appointmentStorage;
            this.patientStorage = patientStorage;
            this.messageCreator = messageCreator;
            this.bus = bus;
        }

        public ILogger Logger
        {
            get { return logger; }
            set { logger = value; }
        }

        public void CreateReminders()
        {
            var appointments = appointmentStorage.GetAppointmentsRequiringReminders();
            logger.DebugFormat("{0} appointments found requiring reminder", appointments.Count());
            var smsCount = 0;
            foreach (var appointment in appointments)
            {
                var patientContacts = patientStorage.GetPatientContacts(appointment.PatientId);
                if(PatientHasContacts(new Tuple<Guid, IEnumerable<PatientContact>>(appointment.PatientId, patientContacts), appointment.Id))
                {
                    foreach (var patientContact in patientContacts)
                    {
                        if (patientContact.IsContactAllowed)
                        {
                            var messageText = messageCreator.CreateReminderSms(appointment);
                            var smsRequested = new SendSmsRequested(patientContact.PhoneNumber, messageText, appointment.Id);
                            bus.Publish(smsRequested);
                            var billableSms = new BillableSmsRequested(appointment.Id, smsRequested.Id,
                                                appointment.SourceSystemId, smsRequested.MessageContent.Length);
                            bus.Publish(billableSms);
                            smsCount++;
                        }
                        else
                        {
                            bus.Publish(new ContactRefused(patientContact.Id, appointment.Id));
                        }
                    }
                }
            }

            logger.DebugFormat("{0} sms requested for appointments found requiring reminder", smsCount);
        }

        private bool PatientHasContacts(Tuple<Guid, IEnumerable<PatientContact>> patientContacts, Guid appointmentId)
        {
            if (patientContacts.Item2.Any())
            {
                bus.Publish(new ValidContactDetailsFound (patientContacts.Item1, appointmentId));
                return true;
            }

            bus.Publish(new ValidContactDetailsNotFound (patientContacts.Item1, appointmentId));
            return false;
        }
    }
}
