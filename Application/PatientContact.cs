using System;

namespace RefactoringDemo
{
    public class PatientContact
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public bool IsContactAllowed { get; set; }
        public string PhoneNumber { get; set; }
    }
}
