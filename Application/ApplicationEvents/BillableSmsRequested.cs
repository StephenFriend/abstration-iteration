using System;

namespace RefactoringDemo.ApplicationEvents
{
    public class BillableSmsRequested : ApplicationEventBase
    {
        public BillableSmsRequested(Guid correlationId, Guid smsId, int sourceSystemId, int messageLength)
        {
            CorrelationId = correlationId;
            SmsId = smsId;
            SourceSystemId = sourceSystemId;
            MessageLength = messageLength;
        }

        public Guid CorrelationId { get; set; }
        public Guid SmsId { get; set; }
        public int SourceSystemId { get; set; }
        public int MessageLength { get; set; }
    }
}
