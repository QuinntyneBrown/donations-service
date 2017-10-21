using DonationsService.Model;
using DonationsService.Features.Core;
using System;

namespace DonationsService.Features.Donors
{

    public class AddedOrUpdatedDonorMessage : BaseEventBusMessage
    {
        public AddedOrUpdatedDonorMessage(Donor donor, Guid correlationId, Guid tenantId)
        {
            Payload = new { Entity = donor, CorrelationId = correlationId };
            TenantUniqueId = tenantId;
        }
        public override string Type { get; set; } = DonorsEventBusMessages.AddedOrUpdatedDonorMessage;        
    }
}
