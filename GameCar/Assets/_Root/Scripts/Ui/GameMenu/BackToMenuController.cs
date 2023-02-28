using Profile;
using Tool;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Ui
{
    internal class BackToMenuController : BaseController
    {
        private readonly ResourcePath _path = new ResourcePath("Prefabs/Ui/BackToMenu");
        private readonly ProfilePlayer _profilePlayer;


        public BackToMenuController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;

            BackToMenuView view = LoadView(placeForUi);
            view.Init(Back);
        }


        private BackToMenuView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_path);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<BackToMenuView>();
        }

        private void Back() =>
            _profilePlayer.CurrentState.Value = GameState.Menu;
    }
}