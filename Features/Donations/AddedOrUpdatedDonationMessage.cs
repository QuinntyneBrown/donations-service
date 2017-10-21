using DonationsService.Model;
using DonationsService.Features.Core;
using System;

namespace DonationsService.Features.Donations
{

    public class AddedOrUpdatedDonationMessage : BaseEventBusMessage
    {
        public AddedOrUpdatedDonationMessage(Donation donation, Guid correlationId, Guid tenantId)
        {
            Payload = new { Entity = donation, CorrelationId = correlationId };
            TenantUniqueId = tenantId;
        }
        public override string Type { get; set; } = DonationsEventBusMessages.AddedOrUpdatedDonationMessage;        
    }
}
