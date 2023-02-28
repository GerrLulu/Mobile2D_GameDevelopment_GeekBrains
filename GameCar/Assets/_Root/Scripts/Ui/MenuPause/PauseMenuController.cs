using Profile;
using Tool;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Ui
{
    internal class PauseMenuController : BaseController
    {
        private readonly ResourcePath _path = new ResourcePath("Prefabs/Ui/PauseMenu");
        private readonly ProfilePlayer _profilePlayer;
        private readonly PauseMenuView _view;
        private readonly Pause _pause;


        public PauseMenuController(Transform placeForUi, ProfilePlayer profilePlayer, Pause pause)
        {
            _profilePlayer = profilePlayer;

            _pause = pause;
            Subscribe(pause);

            _view = LoadView(placeForUi);
            _view.Init(Game, Menu);

            if(_pause.IsEnable)
                _view.Show();
            else
                _view.Hide();
        }


        protected override void OnDispose()
        {
            base.OnDispose();
            Unsubscribe(_pause);
        }


        private void Subscribe(Pause pause)
        {
            pause.Enabled += OnPauseEnabled;
            pause.Disabled += OnPauseDisable;
        }

        private void Unsubscribe(Pause pause)
        {
            pause.Enabled -= OnPauseEnabled;
            pause.Disabled -= OnPauseDisable;
        }

        private void OnPauseEnabled() => _view.Show();

        private void OnPauseDisable() => _view.Hide();


        private PauseMenuView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_path);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<PauseMenuView>();
        }


        private void Game() => _pause.Disable();
        private void Menu() => _profilePlayer.CurrentState.Value = GameState.Menu;
    }
}
