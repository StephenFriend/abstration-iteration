namespace RefactoringDemo
{
    public interface IMessageCreator
    {
        string CreateReminderSms(Appointment appointment);
    }
}