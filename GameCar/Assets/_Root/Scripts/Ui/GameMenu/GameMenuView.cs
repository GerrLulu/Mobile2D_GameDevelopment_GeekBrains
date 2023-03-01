using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Ui
{
    internal class GameMenuView : MonoBehaviour
    {
        [SerializeField] private Button _buttonBackToMenu;
        [SerializeField] private Button _buttonPause;


        public void Init(UnityAction back, UnityAction pause)
        {
            _buttonBackToMenu.onClick.AddListener(back);
            _buttonPause.onClick.AddListener(pause);
        }

        private void OnDestroy()
        {
            _buttonBackToMenu.onClick.RemoveAllListeners();
            _buttonPause.onClick.RemoveAllListeners();
        }
    }
}