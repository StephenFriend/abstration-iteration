using System;

namespace RefactoringDemo.ApplicationEvents
{
    public class SendSmsRequested : ApplicationEventBase
    {
        public SendSmsRequested(string recipient, string messageContent, Guid correlationId)
        {
            Recipient = recipient;
            CorrelationId = correlationId;
            MessageContent = messageContent;
        }

        public Guid CorrelationId { get; set; }

        public string Recipient { get; set; }

        public string MessageContent { get; set; }

    }
}
