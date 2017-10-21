using DonationsService.Features.Core;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json.Linq;
using System;

namespace DonationsService.Features.Donors
{
    public interface IDonorsEventBusMessageHandler: IEventBusMessageHandler { }

    public class DonorsEventBusMessageHandler: IDonorsEventBusMessageHandler
    {
        public DonorsEventBusMessageHandler(ICache cache)
        {
            _cache = cache;
        }

        public void Handle(JObject message)
        {
            try
            {
                if ($"{message["type"]}" == DonorsEventBusMessages.AddedOrUpdatedDonorMessage)
                {
                    _cache.Remove(DonorsCacheKeyFactory.Get(new Guid(message["tenantUniqueId"].ToString())));
                }

                if ($"{message["type"]}" == DonorsEventBusMessages.RemovedDonorMessage)
                {
                    _cache.Remove(DonorsCacheKeyFactory.Get(new Guid(message["tenantUniqueId"].ToString())));
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
