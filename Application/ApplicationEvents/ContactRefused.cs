using System;

namespace RefactoringDemo.ApplicationEvents
{
    public class ContactRefused : ApplicationEventBase
    {
        public ContactRefused(Guid patientContactId, Guid correlationId)
        {
            this.PatientContactId = patientContactId;
            this.CorrelationId = correlationId;
        }
        public Guid PatientContactId { get; set; }
        public Guid CorrelationId { get; set; }
    }
}