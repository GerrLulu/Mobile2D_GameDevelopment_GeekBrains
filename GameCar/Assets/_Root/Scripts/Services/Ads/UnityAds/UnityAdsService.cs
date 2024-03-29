﻿using Services.Ads.UnityAds.Settings;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Events;

namespace Services.Ads.UnityAds
{
    internal sealed class UnityAdsService : MonoBehaviour, IUnityAdsInitializationListener, IAdsServices
    {
        [Header("Components")]
        [SerializeField] private UnityAdsSettings _settings;

        [field: SerializeField] public UnityEvent Initialized { get; private set; }

        public IAdsPlayer IntersititialPlayer { get; private set; }
        public IAdsPlayer RewardedPlayer { get; private set; }
        public IAdsPlayer BannerPlayer { get; private set; }
        public bool IsInitialized => Advertisement.isInitialized;


        private void Awake()
        {
            InitializeAds();
            InitializePlayer();
        }

        private void InitializeAds() =>
            Advertisement.Initialize(
                _settings.GameId,
                _settings.TestMode,
                this);

        private void InitializePlayer()
        {
            IntersititialPlayer = CreateInterstitial();
            RewardedPlayer = CreateRewarded();
            BannerPlayer = CreateBanner();
        }


        private IAdsPlayer CreateInterstitial() =>
            _settings.Intersitial.Enable
            ? new IntersititialPlayer(_settings.Intersitial.Id)
            : new StubPlayer("");

        private IAdsPlayer CreateRewarded() =>
            _settings.Rewarded.Enable
            ? new RewardedPlayer(_settings.Rewarded.Id)
            : new StubPlayer("");

        private IAdsPlayer CreateBanner() =>
            new StubPlayer("");


        void IUnityAdsInitializationListener.OnInitializationComplete()
        {
            Log("Initialization complete");
            Initialized?.Invoke();
        }

        void IUnityAdsInitializationListener.OnInitializationFailed(UnityAdsInitializationError error, string message) =>
            Error($"Initialization Failed: {error.ToString()} - {message}");


        private void Log(string message) => Debug.Log(WrapMessage(message));
        private void Error(string message) => Debug.LogError(WrapMessage(message));
        private string WrapMessage(string message) => $"[{GetType().Name}] {message}";
    }
}