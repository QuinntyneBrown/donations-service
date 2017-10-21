using DonationsService.Features.Core;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json.Linq;
using System;

namespace DonationsService.Features.Donations
{
    public interface IDonationsEventBusMessageHandler: IEventBusMessageHandler { }

    public class DonationsEventBusMessageHandler: IDonationsEventBusMessageHandler
    {
        public DonationsEventBusMessageHandler(ICache cache)
        {
            _cache = cache;
        }

        public void Handle(JObject message)
        {
            try
            {
                if ($"{message["type"]}" == DonationsEventBusMessages.AddedOrUpdatedDonationMessage)
                {
                    _cache.Remove(DonationsCacheKeyFactory.Get(new Guid(message["tenantUniqueId"].ToString())));
                }

                if ($"{message["type"]}" == DonationsEventBusMessages.RemovedDonationMessage)
                {
                    _cache.Remove(DonationsCacheKeyFactory.Get(new Guid(message["tenantUniqueId"].ToString())));
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private readonly ICache _cache;
    }
}
