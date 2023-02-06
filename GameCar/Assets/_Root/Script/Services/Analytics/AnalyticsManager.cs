using Services.Analytics.UnityAnalytics;
using System.Collections.Generic;
using UnityEngine;

namespace Services.Analytics
{
    internal sealed class AnalyticsManager : MonoBehaviour
    {
        private IAnalyticsService[] _services;

        private void Awake()
        {
            _services = new IAnalyticsService[]
            {
                new UnityAnalyticsService()
            };
        }

        public void SendStartGameEvent() =>
            SendEvent("StartGame");

        public void SendMainMenuOpenedEvent() =>
            SendEvent("MainMenuOpened");

        public void SendTranzaction(string productID, decimal amount, string currency) =>
            SendEventBuy(productID, amount, currency);


        private void SendEvent(string eventName)
        {
            foreach (IAnalyticsService service in _services)
                service.SendEvent(eventName);
        }

        private void SendEvent(string eventName, Dictionary<string, object> eventDate)
        {
            foreach(IAnalyticsService service in _services)
                service.SendEvent(eventName, eventDate);
        }

        private void SendEventBuy(string productID, decimal amount, string currency)
        {
            foreach (IAnalyticsService service in _services)
                service.SendEnebtBuy(productID, amount, currency);
        }
    }
}
