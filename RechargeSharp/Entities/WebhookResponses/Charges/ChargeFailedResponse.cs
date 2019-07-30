﻿using Newtonsoft.Json;

namespace RechargeSharp.Entities.WebhookResponses.Charges
{
    public class ChargeFailedResponse
    {
        [JsonProperty("charge")]
        public WebhookChargeFailed Charge { get; set; }
    }
}
