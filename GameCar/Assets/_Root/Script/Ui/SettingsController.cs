using GameCarProfile;
using GameCarTool;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameCarUI
{
    internal sealed class SettingsController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/Settings");
        private readonly ProfilePlayer _profilePlayer;
        private readonly SettingsView _view;

        public SettingsController(Transform placeForUI, ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;
            _view = LoadView(placeForUI);
            _view.InitMainMenu(MainMenuGame);
        }

        private SettingsView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<SettingsView>();
        }

        private void MainMenuGame() =>
            _profilePlayer.CurrentState.Value = GameState.MainMenu;
    }
}