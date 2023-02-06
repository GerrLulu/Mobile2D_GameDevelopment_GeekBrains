using Services.Ads;
using Services.Ads.UnityAds;
using Services.IAP;
using UnityEngine;

namespace Services
{
    internal sealed class ServiceRoster : MonoBehaviour
    {
        private static ServiceRoster _instance;
        private static ServiceRoster Instance =>
            _instance ??= FindObjectOfType<ServiceRoster>();


        public static IAdsServices AdsServices => Instance._adsService;
        public static IIAPService IAPService => Instance._iapService;
       
        [SerializeField] private UnityAdsService _adsService;
        [SerializeField] private IAPService _iapService;

        private void Awake() => _instance ??= this;
    }
}