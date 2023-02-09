using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace GameCarUI
{
    public sealed class SettingsView : MonoBehaviour
    {
        [SerializeField] private Button _buttonBack;

        private UnityAction _backGameCache;

        public void InitMainMenu(UnityAction mainMenuGame)
        {
            _backGameCache = mainMenuGame;
            _buttonBack.onClick.AddListener(mainMenuGame);
        }

        public void OnDestroy()
        {
            _buttonBack.onClick.RemoveListener(_backGameCache);
        }
    }
}