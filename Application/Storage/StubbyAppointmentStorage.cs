using System;
using System.Collections.Generic;

namespace RefactoringDemo.Storage
{
    public class StubbyAppointmentStorage : IAppointmentStorage
    {
        public IEnumerable<Appointment> GetAppointmentsRequiringReminders()
        {
            return new List<Appointment>
            {
                new Appointment
                {
                    Id = Guid.NewGuid(),
                    PatientId = Guid.NewGuid(),
                    SourceSystemId = 1,
                    Location = "Location One",
                    Start = DateTime.Now.AddDays(1),
                    End = DateTime.Now.AddDays(1).AddHours(1)
                },
                new Appointment
                {
                    Id = Guid.NewGuid(),
                    PatientId = Guid.NewGuid(),
                    SourceSystemId = 1,
                    Location = "Location Two",
                    Start = DateTime.Now.AddDays(2),
                    End = DateTime.Now.AddDays(2).AddHours(1)
                },
                new Appointment
                {
                    Id = Guid.NewGuid(),
                    PatientId = Guid.NewGuid(),
                    SourceSystemId = 1,
                    Location = "Location One",
                    Start = DateTime.Now.AddDays(3),
                    End = DateTime.Now.AddDays(3).AddHours(1)
                },
                new Appointment
                {
                    Id = Guid.NewGuid(),
                    PatientId = Guid.NewGuid(),
                    SourceSystemId = 1,
                    Location = "Location 3",
                    Start = DateTime.Now.AddDays(1),
                    End = DateTime.Now.AddDays(1).AddHours(1)
                }
            };
        }
    }
}
