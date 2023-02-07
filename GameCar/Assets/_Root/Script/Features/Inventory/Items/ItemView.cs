using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Features.Inventory.Items
{
    internal interface IItemView
    {
        void Init(IItem item, UnityAction clicAction);
        public void Select();
        public void Unselect();
    }


    internal sealed class ItemView : MonoBehaviour, IItemView
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Button _button;

        [SerializeField] private GameObject _selectBackground;
        [SerializeField] private GameObject _unselectForeground;

        private UnityAction _clicActionCache;


        private void OnDestroy() => Deinit();


        public void Init(IItem item, UnityAction clicAction)
        {
            _text.text = item.Info.Title;
            _icon.sprite = item.Info.Icon;

            _clicActionCache = clicAction;
            _button.onClick.AddListener(clicAction);
        }

        public void Deinit()
        {
            _text.text = string .Empty;
            _icon.sprite = null;
            _button.onClick.RemoveListener(_clicActionCache);
        }


        public void Select() => SetSelected(true);
        public void Unselect() => SetSelected(false);

        private void SetSelected(bool isSelected)
        {
            _selectBackground.SetActive(isSelected);
            _unselectForeground.SetActive(!isSelected);
        }
    }
}