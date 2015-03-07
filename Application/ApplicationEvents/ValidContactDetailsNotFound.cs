using System;

namespace RefactoringDemo.ApplicationEvents
{
    public class ValidContactDetailsNotFound : ApplicationEventBase
    {
        public ValidContactDetailsNotFound(Guid patientId, Guid correlationId)
        {
            PatientId = patientId;
            CorrelationId = correlationId;
        }
        public Guid PatientId { get; set; }

        public Guid CorrelationId { get; set; }
    }
}
