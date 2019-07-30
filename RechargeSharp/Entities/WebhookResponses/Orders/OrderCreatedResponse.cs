﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace RechargeSharp.Entities.WebhookResponses.Orders
{
    public class OrderCreatedResponse
    {
        [JsonProperty("order")]
        public WebhookOrder Order { get; set; }
    }
}
