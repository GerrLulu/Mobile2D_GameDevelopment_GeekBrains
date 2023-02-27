using UnityEngine.Events;

namespace Services.Ads
{
    internal interface IAdsServices
    {
        IAdsPlayer IntersititialPlayer { get; }
        IAdsPlayer RewardedPlayer { get; }
        IAdsPlayer BannerPlayer { get; }
        UnityEvent Initialized { get; }
        bool IsInitialized { get; }
    }
}