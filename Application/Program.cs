using System;
using Castle.Windsor;

namespace RefactoringDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new WindsorContainer().Install(new Installer());
            var reminderCreator = container.Resolve<ReminderCreator>();

            reminderCreator.CreateReminders();

            Console.ReadKey();
        }
    }
}
