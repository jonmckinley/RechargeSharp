﻿using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace RechargeSharp.Entities.Shared
{
    public class CheckoutLineItem : IEquatable<CheckoutLineItem>
    {
        public bool Equals(CheckoutLineItem other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Grams == other.Grams && Images == other.Images && Price == other.Price && Properties.SequenceEqual(other.Properties) && Quantity == other.Quantity && ShopifyProductId == other.ShopifyProductId && ShopifyVariantId == other.ShopifyVariantId && Sku == other.Sku && SubscriptionId == other.SubscriptionId && Title == other.Title && VariantTitle == other.VariantTitle;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((CheckoutLineItem) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Grams.GetHashCode();
                hashCode = (hashCode * 397) ^ (Images != null ? Images.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Price != null ? Price.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Properties != null ? Properties.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Quantity.GetHashCode();
                hashCode = (hashCode * 397) ^ ShopifyProductId.GetHashCode();
                hashCode = (hashCode * 397) ^ (ShopifyVariantId != null ? ShopifyVariantId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Sku != null ? Sku.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ SubscriptionId.GetHashCode();
                hashCode = (hashCode * 397) ^ (Title != null ? Title.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (VariantTitle != null ? VariantTitle.GetHashCode() : 0);
                return hashCode;
            }
        }

        public static bool operator ==(CheckoutLineItem left, CheckoutLineItem right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(CheckoutLineItem left, CheckoutLineItem right)
        {
            return !Equals(left, right);
        }

        [JsonProperty("grams")]
        public long Grams { get; set; }

        [JsonProperty("images")]
        public Images Images { get; set; }

        [JsonProperty("price")]
        public string Price { get; set; }

        [JsonProperty("properties")]
        public Dictionary<string,string> Properties { get; set; }

        [JsonProperty("quantity")]
        public long Quantity { get; set; }

        [JsonProperty("shopify_product_id")]
        public long ShopifyProductId { get; set; }

        [JsonProperty("shopify_variant_id")]
        public string ShopifyVariantId { get; set; }

        [JsonProperty("sku")]
        public string Sku { get; set; }

        [JsonProperty("subscription_id")]
        public long SubscriptionId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("variant_title")]
        public string VariantTitle { get; set; }
    }
}