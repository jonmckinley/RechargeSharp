﻿using RechargeSharp.Entities.Charges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RechargeSharp.Entities.Shared;

namespace RechargeSharp.Services.Charges
{
    public class ChargeService : RechargeSharpService
    {
        public ChargeService(string apiKey) : base(apiKey)
        {
        }

        public async Task<Charge> GetChargeAsync(long id)
        {
            var response = await GetAsync($"/charges/{id}").ConfigureAwait(false);
            return JsonConvert.DeserializeObject<ChargeResponse>(
                await response.Content.ReadAsStringAsync().ConfigureAwait(false)).Charge;
        }

        private async Task<IEnumerable<Charge>> GetChargesAsync(string queryParams)
        {
            var response = await GetAsync($"/charges?{queryParams}").ConfigureAwait(false);
            return JsonConvert.DeserializeObject<ChargeListResponse>(
                await response.Content.ReadAsStringAsync().ConfigureAwait(false)).Charges;
        }

        public Task<IEnumerable<Charge>> GetChargesAsync(int page = 1, int limit = 50, string status = null, long? customerId = null, long? addressId = null, long? shopifyOrderId = null, long? subscriptionId = null, DateTime? date = null, DateTime? dateMin = null, DateTime? dateMax = null, DateTime? createdAtMin = null, DateTime? createAtMax = null, DateTime? updatedAtMin = null, DateTime? updatedAtMax = null)
        {
            var queryParams = $"page={page}&limit={limit}";
            queryParams += status != null ? $"&status={status}" : "";
            queryParams += customerId != null ? $"&customer_id={customerId}" : "";
            queryParams += addressId != null ? $"&address_id={addressId}" : "";
            queryParams += shopifyOrderId != null ? $"&shopify_order_id={shopifyOrderId}" : "";
            queryParams += subscriptionId != null ? $"&subscription_id={subscriptionId}" : "";
            queryParams += date != null ? $"&date={date?.ToString("s")}" : "";
            queryParams += dateMin != null ? $"&date_min={dateMin?.ToString("s")}" : "";
            queryParams += dateMax != null ? $"&date_max={dateMax?.ToString("s")}" : "";
            queryParams += createdAtMin != null ? $"&created_at_min={createdAtMin?.ToString("s")}" : "";
            queryParams += createAtMax != null ? $"&created_at_max={createAtMax?.ToString("s")}" : "";
            queryParams += updatedAtMin != null ? $"&updated_at_min={updatedAtMin?.ToString("s")}" : "";
            queryParams += updatedAtMax != null ? $"&updated_at_max={updatedAtMax?.ToString("s")}" : "";

            return GetChargesAsync(queryParams);
        }

        public Task<IEnumerable<Charge>> GetAllChargesWithParamsAsync(string status = null, long? customerId = null, long? addressId = null, long? shopifyOrderId = null, long? subscriptionId = null, DateTime? date = null, DateTime? dateMin = null, DateTime? dateMax = null, DateTime? createdAtMin = null, DateTime? createAtMax = null, DateTime? updatedAtMin = null, DateTime? updatedAtMax = null)
        {
            var queryParams = "";
            queryParams += status != null ? $"&status={status}" : "";
            queryParams += customerId != null ? $"&customer_id={customerId}" : "";
            queryParams += addressId != null ? $"&address_id={addressId}" : "";
            queryParams += shopifyOrderId != null ? $"&shopify_order_id={shopifyOrderId}" : "";
            queryParams += subscriptionId != null ? $"&subscription_id={subscriptionId}" : "";
            queryParams += date != null ? $"&date={date?.ToString("s")}" : "";
            queryParams += dateMin != null ? $"&date_min={dateMin?.ToString("s")}" : "";
            queryParams += dateMax != null ? $"&date_max={dateMax?.ToString("s")}" : "";
            queryParams += createdAtMin != null ? $"&created_at_min={createdAtMin?.ToString("s")}" : "";
            queryParams += createAtMax != null ? $"&created_at_max={createAtMax?.ToString("s")}" : "";
            queryParams += updatedAtMin != null ? $"&updated_at_min={updatedAtMin?.ToString("s")}" : "";
            queryParams += updatedAtMax != null ? $"&updated_at_max={updatedAtMax?.ToString("s")}" : "";

            return GetAllChargesAsync(queryParams);
        }

        private async Task<IEnumerable<Charge>> GetAllChargesAsync(string queryParams)
        {
            var count = await CountChargesAsync();

            var taskList = new List<Task<IEnumerable<Charge>>>();

            var pages = Math.Ceiling(Convert.ToDouble(count) / 250d);

            for (int i = 1; i <= Convert.ToInt32(pages); i++)
            {
                taskList.Add(GetChargesAsync($"page={i}&limit=250"+ queryParams));
            }

            var computed = await Task.WhenAll(taskList);

            var result = new List<Charge>();

            foreach (var charges in computed)
            {
                result.AddRange(charges);
            }

            return result;
        }

        public async Task<long> CountChargesAsync()
        {
            var response = await GetAsync("/charges/count").ConfigureAwait(false);
            return JsonConvert.DeserializeObject<CountResponse>(
                await response.Content.ReadAsStringAsync().ConfigureAwait(false)).Count;
        }

        public async Task<Charge> ChangeNextChargeDateAsync(long chargeId, ChangeNextChargeDateRequest changeNextChargeDateRequest)
        {
            ValidateModel(changeNextChargeDateRequest);

            var response = await PostAsync($"/charges/{chargeId}/change_next_charge_date", JsonConvert.SerializeObject(changeNextChargeDateRequest)).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<ChargeResponse>(
                await response.Content.ReadAsStringAsync().ConfigureAwait(false)).Charge;
        }

        public async Task<Charge> SkipNextChargeAsync(long chargeId, SkipNextChargeRequest skipNextChargeRequest)
        {
            ValidateModel(skipNextChargeRequest);

            var response = await PostAsync($"/charges/{chargeId}/skip", JsonConvert.SerializeObject(skipNextChargeRequest)).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<ChargeResponse>(
                await response.Content.ReadAsStringAsync().ConfigureAwait(false)).Charge;
        }

        public async Task<Charge> UnskipNextChargeAsync(long chargeId, SkipNextChargeRequest skipNextChargeRequest)
        {
            ValidateModel(skipNextChargeRequest);

            var response = await PostAsync($"/charges/{chargeId}/unskip", JsonConvert.SerializeObject(skipNextChargeRequest)).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<ChargeResponse>(
                await response.Content.ReadAsStringAsync().ConfigureAwait(false)).Charge;
        }

        public async Task<Charge> RefundChargeAsync(long chargeId, RefundChargeRequest refundChargeRequest)
        {
            ValidateModel(refundChargeRequest);

            var response = await PostAsync($"/charges/{chargeId}/refund", JsonConvert.SerializeObject(refundChargeRequest)).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<ChargeResponse>(
                await response.Content.ReadAsStringAsync().ConfigureAwait(false)).Charge;
        }

        public async Task<Charge> TotalRefundChargeAsync(long chargeId, TotalRefundChargeRequest totalRefundChargeRequest)
        {
            ValidateModel(totalRefundChargeRequest);

            var response = await PostAsync($"/charges/{chargeId}/refund", JsonConvert.SerializeObject(totalRefundChargeRequest)).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<ChargeResponse>(
                await response.Content.ReadAsStringAsync().ConfigureAwait(false)).Charge;
        }

        public async Task DeleteChargeAsync(long id)
        {
            var response = await DeleteAsync($"/charges/{id}").ConfigureAwait(false);
        }


    }
}
