using System;

namespace RefactoringDemo.ApplicationEvents
{
    public class ValidContactDetailsFound : ApplicationEventBase
    {
        public ValidContactDetailsFound(Guid patientId, Guid correlationId)
        {
            PatientId = patientId;
            CorrelationId = correlationId;
        }

        public Guid PatientId { get; set; }

        public Guid CorrelationId { get; set; }
    }
}
