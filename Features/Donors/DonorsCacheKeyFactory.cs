using System;

namespace DonationsService.Features.Donors
{
    public class DonorsCacheKeyFactory
    {
        public static string Get(Guid tenantId) => $"[Donors] Get {tenantId}";
        public static string GetById(Guid tenantId, int id) => $"[Donors] GetById {tenantId}-{id}";
    }
}
