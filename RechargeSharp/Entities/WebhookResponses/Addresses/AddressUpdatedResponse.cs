﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace RechargeSharp.Entities.WebhookResponses.Addresses
{
    public class AddressUpdatedResponse
    {
        [JsonProperty("address")]
        public WebhookAddress Address { get; set; }
    }
}
