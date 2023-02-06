using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace GameCarUI
{
    public sealed class MainMenuView : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private string _productID;

        [Header("Buttons")]
        [SerializeField] private Button _buttonStart;
        [SerializeField] private Button _buttonSettings;
        [SerializeField] private Button _buttonRewarded;
        [SerializeField] private Button _buttonBuy;

        private UnityAction _startGameCache;
        private UnityAction _settingsGameCache;
        private UnityAction _adsRewardedGameCache;

        public void Init(UnityAction startGame, UnityAction settingsGame,
            UnityAction adsRewardedGame, UnityAction<string> buyProductGame)
        {
            _startGameCache = startGame;
            _settingsGameCache = settingsGame;
            _adsRewardedGameCache = adsRewardedGame;
            
            _buttonStart.onClick.AddListener(startGame);
            _buttonSettings.onClick.AddListener(settingsGame);
            _buttonRewarded.onClick.AddListener(adsRewardedGame);
            _buttonBuy.onClick.AddListener(() => buyProductGame(_productID));
        }

        public void OnDestroy() 
        {
            _buttonStart.onClick.RemoveListener(_startGameCache);
            _buttonSettings.onClick.RemoveListener(_settingsGameCache);
            _buttonRewarded.onClick.RemoveListener(_adsRewardedGameCache);
            _buttonBuy.onClick.RemoveAllListeners();
        }
    }
}