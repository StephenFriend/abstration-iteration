using System;
using System.Collections.Generic;

namespace RefactoringDemo.Storage
{
    public class StubbyPatientStorage : IPatientStorage
    {
        public IEnumerable<PatientContact> GetPatientContacts(Guid patientId)
        {
            var contacts = new List<PatientContact>
            {
                new PatientContact
                {
                    PatientId = patientId, Id = Guid.NewGuid(), IsContactAllowed = true, PhoneNumber = "07700900000"
                }
            };
            
            int output;

            if(int.TryParse(patientId.ToString()[0].ToString(), out output))
            {
                contacts.Add(new PatientContact
                {
                    Id = Guid.NewGuid(),
                    PatientId = patientId,
                    IsContactAllowed = false,
                    PhoneNumber = "07700900999"
                });
            }

            return contacts;
        }
    }
}