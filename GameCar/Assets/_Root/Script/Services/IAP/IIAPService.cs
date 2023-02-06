using UnityEngine.Events;

namespace Services.IAP
{
    internal interface IIAPService
    {
        UnityEvent Initialized { get; }
        UnityEvent PurchaseSucced { get; }
        UnityEvent PurchaseFailed { get; }
        bool IsInitialized { get; }

        void Buy(string id);
        string GetCost(string productID);
        void RestorePurchase();
    }
}
