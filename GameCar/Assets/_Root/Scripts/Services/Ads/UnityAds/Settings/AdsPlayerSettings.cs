using System;
using UnityEngine;

namespace Services.Ads.UnityAds.Settings
{
    [Serializable]
    internal class AdsPlayerSettings
    {
        [field: SerializeField] public bool Enable { get; private set; }
        [SerializeField] private string _androidID;
        [SerializeField] private string _iosID;

        public string Id =>
#if UNITY_EDITOR
            _androidID;
#else
        Application.platform switch
            {
                RuntimePlatform.Android => _androidID,
                //RuntimePlatform.IPhonePlayer => _iOsID,
                _ => ""
            };
#endif
    }
}