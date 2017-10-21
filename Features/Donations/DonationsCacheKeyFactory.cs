using System;

namespace DonationsService.Features.Donations
{
    public class DonationsCacheKeyFactory
    {
        public static string Get(Guid tenantId) => $"[Donations] Get {tenantId}";
        public static string GetById(Guid tenantId, int id) => $"[Donations] GetById {tenantId}-{id}";
    }
}
