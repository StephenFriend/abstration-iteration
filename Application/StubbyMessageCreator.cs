namespace RefactoringDemo
{
    public class StubbyMessageCreator : IMessageCreator
    {
        public string CreateReminderSms(Appointment appointment)
        {
            return string.Format("Appointment reminder: {0} at {1}", appointment.Location, appointment.Start);
        }
    }
}
