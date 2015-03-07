using System;
using System.Collections.Generic;

namespace RefactoringDemo.Storage
{
    public interface IPatientStorage
    {
        IEnumerable<PatientContact> GetPatientContacts(Guid patientId);
    }
}
