using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace RefactoringDemo.Specs
{
    public class AppointmentHelper
    {
        public void CreateAppointmentForPatient(Guid patientId)
        {
            var appointment = new Appointment
            {
                PatientId = patientId,
                Id = Guid.NewGuid(),
                Location = "Location 1",
                SourceSystemId = 101,
                Start = DateTime.Now.AddDays(3)
            };

            ScenarioContext.Current.Get<List<Appointment>>(ScenarioContextKeys.Appointments).Add(appointment);
        }
    }
}