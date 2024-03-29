﻿using UnityEngine.Advertisements;

namespace Services.Ads.UnityAds
{
    internal sealed class RewardedPlayer : UnityAdsPlayer
    {
        public RewardedPlayer(string Id) : base(Id) { }
        protected override void OnPlaying() => Advertisement.Show(Id, this);
        protected override void Load() => Advertisement.Load(Id, this);
    }
}