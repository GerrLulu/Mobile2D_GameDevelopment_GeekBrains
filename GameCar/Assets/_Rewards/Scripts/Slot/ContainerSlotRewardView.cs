using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rewards.Slot
{
    internal class ContainerSlotRewardView : MonoBehaviour
    {
        [SerializeField] private Image _originalBackground;
        [SerializeField] private Image _selectBackground;
        [SerializeField] private Image _iconCurrency;
        [SerializeField] private TMP_Text _textTime;
        [SerializeField] private TMP_Text _countReward;

        [SerializeField] private string _timeKey;


        public void SetData(Reward reward, int countTime, bool isSelected)
        {
            _iconCurrency.sprite = reward.IconCurrency;
            _textTime.text = $"{_timeKey} {countTime}";
            _countReward.text = reward.CountCurrency.ToString();

            UpdateBackground(isSelected);
        }

        private void UpdateBackground(bool isSelect)
        {
            _originalBackground.gameObject.SetActive(!isSelect);
            _selectBackground.gameObject.SetActive(isSelect);
        }
    }
}
