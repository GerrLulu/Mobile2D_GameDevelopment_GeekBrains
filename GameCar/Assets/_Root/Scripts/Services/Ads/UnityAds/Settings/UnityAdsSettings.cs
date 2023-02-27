using UnityEngine;

namespace Services.Ads.UnityAds.Settings
{
    [CreateAssetMenu(fileName = nameof(UnityAdsSettings), menuName = "Settings/Ads/" + nameof(UnityAdsSettings))]
    internal sealed class UnityAdsSettings : ScriptableObject
    {
        [Header("Game ID")]
        [SerializeField] private string _androidGameID;
        [SerializeField] private string _iosGameID;

        [field: Header("Players")]
        [field: SerializeField] public AdsPlayerSettings Intersitial { get; private set; }
        [field: SerializeField] public AdsPlayerSettings Rewarded { get; private set; }
        [field: SerializeField] public AdsPlayerSettings Banner { get; private set; }

        [field: Header("Settings")]
        [field: SerializeField] public bool TestMode { get; private set; } = true;


        public string GameId =>
#if UNITY_EDITOR
            _androidGameID;
#else
        Application.platform switch
            {
                RuntimePlatform.Android => _androidGameID,
                //RuntimePlatform.IPhonePlayer => _iOsGameID,
                _ => ""
            };
#endif
    }
}