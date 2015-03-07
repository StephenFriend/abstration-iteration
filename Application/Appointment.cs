using System;

namespace RefactoringDemo
{
    public class Appointment
    {
        public Guid PatientId { get; set; }
        public Guid Id { get; set; }
        public int SourceSystemId { get; set; }
        public string Location { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
            
    }
}
