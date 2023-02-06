using UnityEngine.Advertisements;

namespace Services.Ads.UnityAds
{
    internal sealed class IntersititialPlayer : UnityAdsPlayer
    {
        public IntersititialPlayer(string id) : base(id) { }

        protected override void OnPlaying() => Advertisement.Show(Id, this);
        protected override void Load() => Advertisement.Load(Id, this);
    }
}