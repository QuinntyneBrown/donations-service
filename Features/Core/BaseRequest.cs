using System;

namespace DonationsService.Features.Core
{
    public class BaseRequest 
    {
        public Guid TenantUniqueId { get; set; }
    }
}