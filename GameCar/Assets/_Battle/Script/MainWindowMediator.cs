using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BattleScript
{
    internal class MainWindowMediator : MonoBehaviour
    {
        [Header("Player Stats")]
        [SerializeField] private TMP_Text _countMoneyText;
        [SerializeField] private TMP_Text _countHealthText;
        [SerializeField] private TMP_Text _countPowerText;
        [SerializeField] private TMP_Text _countWantedText;

        [Header("Enemy Stats")]
        [SerializeField] private TMP_Text _countPowerEnemyText;

        [Header("Money Buttons")]
        [SerializeField] private Button _addMoneyButton;
        [SerializeField] private Button _minusMoneyButton;

        [Header("Health Buttons")]
        [SerializeField] private Button _addHealthButton;
        [SerializeField] private Button _minusHealthButton;

        [Header("Power Buttons")]
        [SerializeField] private Button _addPowerButton;
        [SerializeField] private Button _minusPowerButton;

        [Header("Wanted Buttons")]
        [SerializeField] private Button _addWantedButton;
        [SerializeField] private Button _minusWantedButton;

        [Header("Other Buttons")]
        [SerializeField] private Button _fightButton;
        [SerializeField] private Button _peacefulWayButton;


        private PlayerData _money;
        private PlayerData _heath;
        private PlayerData _power;
        private PlayerData _wanted;

        private Enemy _enemy;


        private const int _minWantedToPeacefulWay = 0;
        private const int _maxWantedToPeacefulWay = 2;
        private const int _minWantedToFight = 0;
        private const int _maxWantedToFight = 5;


        private void Start()
        {
            _enemy = new Enemy("Enemy Flappy");

            _money = CreatePlayerData(DataType.Money);
            _heath = CreatePlayerData(DataType.Health);
            _power = CreatePlayerData(DataType.Power);
            _wanted = CreatePlayerData(DataType.Wanted);

            Subscribe();
        }

        private void OnDestroy()
        {
            DisposePlayerData(ref _money);
            DisposePlayerData(ref _heath);
            DisposePlayerData(ref _power);
            DisposePlayerData(ref _wanted);

            Unsubscribe();
        }


        private PlayerData CreatePlayerData(DataType dataType)
        {
            PlayerData playerData = new PlayerData(dataType);
            playerData.Attach(_enemy);

            return playerData;
        }

        private void DisposePlayerData(ref PlayerData playerData)
        {
            playerData.Detach(_enemy);
            playerData = null;
        }


        private void Subscribe()
        {
            _addMoneyButton.onClick.AddListener(IncreaseMoney);
            _minusMoneyButton.onClick.AddListener(DecreaseMoney);

            _addHealthButton.onClick.AddListener(IncreaseHealth);
            _minusHealthButton.onClick.AddListener(DecreaseHealth);

            _addPowerButton.onClick.AddListener(IncreasePower);
            _minusPowerButton.onClick.AddListener(DecreasePower);

            _addWantedButton.onClick.AddListener(IncreaseWanted);
            _minusWantedButton.onClick.AddListener(DecreaseWanted);


            _fightButton.onClick.AddListener(Fight);
            _peacefulWayButton.onClick.AddListener(PeacefulWay);
        }

        private void Unsubscribe()
        {
            _addMoneyButton.onClick.RemoveAllListeners();
            _minusMoneyButton.onClick.RemoveAllListeners();

            _addHealthButton.onClick.RemoveAllListeners();
            _minusHealthButton.onClick.RemoveAllListeners();

            _addPowerButton.onClick.RemoveAllListeners();
            _minusPowerButton.onClick.RemoveAllListeners();

            _addWantedButton.onClick.RemoveAllListeners();
            _addWantedButton.onClick.RemoveAllListeners();


            _fightButton.onClick.RemoveAllListeners();
            _peacefulWayButton.onClick.RemoveAllListeners();
        }


        private void IncreaseMoney() => IncreaseValue(_money);
        private void DecreaseMoney() => DecreaseValue(_money);

        private void IncreaseHealth() => IncreaseValue(_heath);
        private void DecreaseHealth() => DecreaseValue(_heath);

        private void IncreasePower() => IncreaseValue(_power);
        private void DecreasePower() => DecreaseValue(_power);

        private void IncreaseWanted() => IncreaseValue(_wanted);
        private void DecreaseWanted() => DecreaseValue(_wanted);

        private void IncreaseValue(PlayerData playerData) => AddToValue(1, playerData);
        private void DecreaseValue(PlayerData playerData) => AddToValue(-1, playerData);

        private void AddToValue(int addition, PlayerData playerData)
        {
            playerData.Value += addition;
            ChangeDataWindow(playerData);


            if (playerData.DataType == DataType.Wanted)
                UpdatePeasfulWayButtonVisibility();
        }


        private void ChangeDataWindow(PlayerData playerData)
        {
            int value = playerData.Value;
            DataType dataType = playerData.DataType;
            TMP_Text textComponent = GetTextComponent(dataType);
            textComponent.text = $"Player {dataType:F} {value}";

            int enemyPower = _enemy.CalcPower();
            _countPowerEnemyText.text = $"Enemy Power {enemyPower}";
        }

        private TMP_Text GetTextComponent(DataType dataType) =>
            dataType switch
            {
                DataType.Money => _countMoneyText,
                DataType.Health => _countHealthText,
                DataType.Power => _countPowerText,
                DataType.Wanted => _countWantedText,
                _ => throw new ArgumentException($"Wrong {nameof(DataType)}")
            };


        private void Fight()
        {
            int enemyPower = _enemy.CalcPower();
            bool isVictory = _power.Value >= enemyPower;

            string color = isVictory ? "#07FF00" : "#FF0000";
            string message = isVictory ? "Win" : "Lose";

            Debug.Log($"<color={color}>{message}!!!</color>");
        }


        private void UpdatePeasfulWayButtonVisibility()
        {
            int wantedValue = _wanted.Value;
            bool peacefulWay = _minWantedToPeacefulWay <= wantedValue && wantedValue <= _maxWantedToPeacefulWay;
            bool fightWay = _minWantedToFight <= wantedValue && wantedValue <= _maxWantedToFight;

            _peacefulWayButton.interactable = peacefulWay;
            _peacefulWayButton.gameObject.SetActive(fightWay);
        }

        private void PeacefulWay()
        {
            string color = "#07FF00";
            string message = "Peaceful way";

            Debug.Log($"<color={color}>{message}!!!</color>");
        }
    }
}