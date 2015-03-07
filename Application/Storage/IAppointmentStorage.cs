using System.Collections.Generic;

namespace RefactoringDemo.Storage
{
    public interface IAppointmentStorage
    {
        IEnumerable<Appointment> GetAppointmentsRequiringReminders();
    }
}
