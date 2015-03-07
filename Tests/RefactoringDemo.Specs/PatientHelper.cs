using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace RefactoringDemo.Specs
{
    public class PatientHelper
    {
        public void CreateTestPatient()
        {
           ScenarioContext.Current.Set(Guid.NewGuid(), ScenarioContextKeys.PatientOne);
        }

        public void RegisterContacts(Guid patientId, IEnumerable<PatientContact> patientContacts)
        {
           ScenarioContext.Current.Get<List<PatientContact>>(ScenarioContextKeys.PatientContacts)
                                                    .AddRange(patientContacts);
        }
    }
}
