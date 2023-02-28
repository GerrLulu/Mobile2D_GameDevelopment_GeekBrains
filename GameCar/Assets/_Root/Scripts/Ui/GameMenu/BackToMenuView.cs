using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Ui
{
    internal class BackToMenuView : MonoBehaviour
    {
        [SerializeField] private Button _buttonBackToMenu;


        public void Init(UnityAction back) =>
            _buttonBackToMenu.onClick.AddListener(back);

        private void OnDestroy() =>
            _buttonBackToMenu.onClick.RemoveAllListeners();
    }
}