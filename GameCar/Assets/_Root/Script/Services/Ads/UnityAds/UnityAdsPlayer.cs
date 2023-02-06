using System;
using UnityEngine;
using UnityEngine.Advertisements;

namespace Services.Ads.UnityAds
{
    internal abstract class UnityAdsPlayer : IAdsPlayer, IUnityAdsLoadListener, IUnityAdsShowListener
    {
        public event Action Started;
        public event Action Finished;
        public event Action Failed;
        public event Action Skiped;
        public event Action BecomeReady;

        protected readonly string Id;


        protected UnityAdsPlayer(string id)
        {
            Id = id;
            Advertisement.Load(id, this);
            Advertisement.Show(id, this);
        }
            
        public void Play()
        {
            Load();
            OnPlaying();
            Load();
        }

        protected abstract void OnPlaying();
        protected abstract void Load();


        void IUnityAdsLoadListener.OnUnityAdsAdLoaded(string placementId)
        {
            if (IsIdMy(placementId) == false)
                return;

            Log("Loaded");
            BecomeReady?.Invoke();
        }

        void IUnityAdsLoadListener.OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
        {
            if (IsIdMy(placementId) == false)
                return;

            Error($"Initialization Failed: {error.ToString()} - {message}");
            Failed?.Invoke();
        }

        void IUnityAdsShowListener.OnUnityAdsShowStart(string placementId)
        {
            if (IsIdMy(placementId) == false)
                return;

            Log("Started");
            Started?.Invoke();
        }

        void IUnityAdsShowListener.OnUnityAdsShowClick(string placementId)
        {
            if (IsIdMy(placementId) == false)
                return;

            Log("Start on click");
            Started?.Invoke();
        }

        void IUnityAdsShowListener.OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            if (IsIdMy(placementId) == false)
                return;

            switch(showCompletionState)
            {
                case UnityAdsShowCompletionState.COMPLETED:
                    Log("Finished");
                    Finished?.Invoke();
                    break;

                case UnityAdsShowCompletionState.SKIPPED:
                    Log("Skiped");
                    Skiped?.Invoke();
                    break;
                case UnityAdsShowCompletionState.UNKNOWN:
                    Error("Error unknow");
                    Failed?.Invoke();
                    break;
            }
        }

        void IUnityAdsShowListener.OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
            if (IsIdMy(placementId) == false)
                return;

            Error($"Initialization Failed: {error.ToString()} - {message}");
            Failed?.Invoke();
        }


        private bool IsIdMy(string id) => Id == id;

        private void Log(string message) => Debug.Log(WrapMessage(message));
        private void Error(string message) => Debug.LogError(WrapMessage(message));
        private string WrapMessage(string message) => $"[{GetType().Name}] {message}";
    }
}