using Services.Analytics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Purchasing;

namespace Services.IAP
{
    internal class IAPService : MonoBehaviour, IStoreListener, IIAPService
    {
        [Header("Component")]
        [SerializeField] private ProductLibrary _productLibrary;

        [field: Header("Event")]
        [field: SerializeField] public UnityEvent Initialized { get; private set; }
        [field: SerializeField] public UnityEvent PurchaseSucced { get; private set; }
        [field: SerializeField] public UnityEvent PurchaseFailed { get; private set; }
        public bool IsInitialized { get; private set; }

        [Header("TranzactionAnalytics")]
        [SerializeField] private AnalyticsManager _analyticsManager;

        private IExtensionProvider _extensionProvider;
        private PurchaseValidator _purchaseValidator;
        private PurchaseRestorer _purchaseRestorer;
        private IStoreController _storeController;


        private void Awake() => InitializeProduct();

        private void InitializeProduct()
        {
            StandardPurchasingModule purchasingModule = StandardPurchasingModule.Instance();
            ConfigurationBuilder builder = ConfigurationBuilder.Instance(purchasingModule);

            foreach (Product product in _productLibrary.Products)
                builder.AddProduct(product.Id, product.ProductType);

            Log("Product initialized");
            UnityPurchasing.Initialize(this, builder);
        }


        void IStoreListener.OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            IsInitialized = true;
            _storeController = controller;
            _extensionProvider = extensions;
            _purchaseValidator = new PurchaseValidator();
            _purchaseRestorer = new PurchaseRestorer(_extensionProvider);

            Log("Initialized");
            Initialized?.Invoke();
        }

        void IStoreListener.OnInitializeFailed(InitializationFailureReason error)
        {
            IsInitialized = false;
            Error("Initialization Failed");
        }

        PurchaseProcessingResult IStoreListener.ProcessPurchase(PurchaseEventArgs purchaseEvent)
        {
            if (_purchaseValidator.Validate(purchaseEvent))
                OnPurchaseSucceed(purchaseEvent.purchasedProduct);
            else
                OnPurchaseFailed(purchaseEvent.purchasedProduct.definition.id, "NoValid");

            return PurchaseProcessingResult.Complete;
        }

        void IStoreListener.OnPurchaseFailed(UnityEngine.Purchasing.Product product, PurchaseFailureReason failureReason) =>
            OnPurchaseFailed(product.definition.id, failureReason.ToString());

        private void OnPurchaseFailed(string product, string reson)
        {
            Error($"Failed {product}: {reson}");
            PurchaseFailed?.Invoke();
        }

        private void OnPurchaseSucceed(UnityEngine.Purchasing.Product product)
        {
            string productId = product.definition.id;
            decimal amount = (decimal)(product.definition.payout?.quantity ?? 1);
            string currency = product.metadata.isoCurrencyCode;
            _analyticsManager.SendTranzaction(productId, amount, currency);

            Log($"Purchased: {productId}");
            PurchaseSucced?.Invoke();
        }


        public void Buy(string id)
        {
            if (IsInitialized)
                _storeController.InitiatePurchase(id);
            else
                Error($"Buy {id} Fail. Not initialized.");
        }

        public string GetCost(string productID)
        {
            UnityEngine.Purchasing.Product product = _storeController.products.WithID(productID);
            return product != null ? product.metadata.localizedPriceString : "N/A";
        }

        public void RestorePurchase()
        {
            if (IsInitialized)
                _purchaseRestorer.Restore();
            else
                Error("RestorePurchase Fail. Not initialized");
        }


        private void Log(string message) => Debug.Log(WrapMessage(message));
        private void Error(string message) => Debug.LogError(WrapMessage(message));
        private string WrapMessage(string message) => $"[{GetType().Name}] {message}";
    }
}
