using DonationsService.Features.Core;
using System;

namespace DonationsService.Features.Donations
{
    public class RemovedDonationMessage : BaseEventBusMessage
    {
        public RemovedDonationMessage(int donationId, Guid correlationId, Guid tenantId)
        {
            Payload = new { Id = donationId, CorrelationId = correlationId };
            TenantUniqueId = tenantId;
        }
        public override string Type { get; set; } = DonationsEventBusMessages.RemovedDonationMessage;        
    }
}
