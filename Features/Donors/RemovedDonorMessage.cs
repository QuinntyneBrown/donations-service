using DonationsService.Features.Core;
using System;

namespace DonationsService.Features.Donors
{
    public class RemovedDonorMessage : BaseEventBusMessage
    {
        public RemovedDonorMessage(int donorId, Guid correlationId, Guid tenantId)
        {
            Payload = new { Id = donorId, CorrelationId = correlationId };
            TenantUniqueId = tenantId;
        }
        public override string Type { get; set; } = DonorsEventBusMessages.RemovedDonorMessage;        
    }
}
