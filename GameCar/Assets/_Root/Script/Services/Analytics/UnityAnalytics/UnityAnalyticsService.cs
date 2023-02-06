using System.Collections.Generic;

namespace Services.Analytics.UnityAnalytics
{
    public sealed class UnityAnalyticsService : IAnalyticsService
    {
        public void SendEvent(string eventName) =>
            UnityEngine.Analytics.Analytics.CustomEvent(eventName);

        public void SendEvent(string eventName, Dictionary<string, object> eventDate) =>
            UnityEngine.Analytics.Analytics.CustomEvent(eventName, eventDate);

        public void SendEnebtBuy(string productId, decimal amount, string currency) =>
            UnityEngine.Analytics.Analytics.Transaction(productId, amount, currency);
    }
}