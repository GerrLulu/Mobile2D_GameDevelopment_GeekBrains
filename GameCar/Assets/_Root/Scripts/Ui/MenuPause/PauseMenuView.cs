using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ui
{
    internal class PauseMenuView : MonoBehaviour
    {
        [SerializeField] private Button _buttonBackToGame;
        [SerializeField] private Button _buttonBackToMenu;


        public void Init(UnityAction game, UnityAction menu)
        {
            _buttonBackToGame.onClick.AddListener(game);
            _buttonBackToMenu.onClick.AddListener(menu);
        }

        private void OnDestroy()
        {
            _buttonBackToGame.onClick.RemoveAllListeners();
            _buttonBackToMenu.onClick.RemoveAllListeners();
        }


        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);
    }
}
