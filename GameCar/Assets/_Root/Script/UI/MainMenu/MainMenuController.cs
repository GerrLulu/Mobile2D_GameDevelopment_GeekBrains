using GameCarProfile;
using GameCarTool;
using Services;
using Services.Analytics;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameCarUI
{
    internal sealed class MainMenuController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/UI/MainMenu");
        private readonly ProfilePlayer _profilePlayer;
        private readonly MainMenuView _view;


        public MainMenuController(Transform placeForUi, ProfilePlayer profilePlayer,
            AnalyticsManager analyticsManager)
        {
            _profilePlayer = profilePlayer;
            _view = LoadView(placeForUi);
            _view.Init(StartGame, SettingsGame, PlayRewardedAds, BuyProduct, OpenShed);

            analyticsManager.SendMainMenuOpenedEvent();
        }

        private void BuyProduct(string productID) =>
            ServiceRoster.IAPService.Buy(productID);

        private MainMenuView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<MainMenuView>();
        }

        private void StartGame() =>
            _profilePlayer.CurrentState.Value = GameState.Game;

        private void SettingsGame() =>
            _profilePlayer.CurrentState.Value = GameState.Settings;

        private void PlayRewardedAds() =>
            ServiceRoster.AdsServices.RewardedPlayer.Play();

        private void OpenShed() =>
            _profilePlayer.CurrentState.Value = GameState.Shed;
    }
}