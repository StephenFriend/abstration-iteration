Feature: AppointmentReminder
	In order to help reduce the number of non-attendances at appointments
	As an administrator
	I want patients to receive reminders for their appointments

Scenario: Patient has two contact numbers and an appointment in the reminder window
	Given there is a patient
	And they have the following contact details
	| PhoneNumber | IsContactAllowed |
	| 07700900000 | true             |
	| 07700900001 | false	         |
	| 07700900999 | true             |
	And they have a future appointment
	When the reminder creator runs
	Then we should send a reminder to each phone number where contact is allowed


Scenario: (non-functional) Patient has two contact numbers and an appointment in the reminder window
Given there is a patient
And they have the following contact details
	| PhoneNumber | IsContactAllowed |
	| 07700900000 | true             |
	| 07700900001 | false	         |
	| 07700900999 | true             |
	And they have a future appointment
	When the reminder creator runs
	Then we should log that 1 'appointments found requiring reminder'
	And we should log that 2 'sms requested for appointments found requiring reminder'
	And we should publish  1 'ValidContactDetailsFound'
	And we should publish 1 'ContactRefused'
	And we should publish 2 'BillableSmsRequested'