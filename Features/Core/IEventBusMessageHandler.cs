﻿using Newtonsoft.Json.Linq;

namespace DonationsService.Features.Core
{
    public interface IEventBusMessageHandler
    {
        void Handle(JObject message);
    }
}