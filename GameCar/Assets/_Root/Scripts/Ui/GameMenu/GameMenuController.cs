using Profile;
using Tool;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Ui
{
    internal class GameMenuController : BaseController
    {
        private readonly ResourcePath _path = new ResourcePath("Prefabs/Ui/GameMenu");
        private readonly ProfilePlayer _profilePlayer;
        private readonly Pause _pause;


        public GameMenuController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;

            GameMenuView view = LoadView(placeForUi);
            view.Init(Back, Pause);

            _pause = new Pause();
            CreatePauseMenuController(placeForUi, profilePlayer, _pause);
        }


        protected override void OnDispose()
        {
            base.OnDispose();

            if (_pause.IsEnable)
                _pause.Disable();
        }


        private GameMenuView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_path);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<GameMenuView>();
        }

        private void Back() =>
            _profilePlayer.CurrentState.Value = GameState.Menu;

        private void Pause()
        {
            _pause.Enable();
            Debug.Log("Push1!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        }


        private PauseMenuController CreatePauseMenuController(Transform placeForUi, ProfilePlayer profilePlayer, Pause pause)
        {
            var pauseMenuController = new PauseMenuController(placeForUi, profilePlayer, pause);
            AddController(pauseMenuController);

            return pauseMenuController;
        }
    }
}