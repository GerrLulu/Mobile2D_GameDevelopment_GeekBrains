using GameCarProfile;
using Services.Ads.UnityAds;
using Services.Analytics;
using UnityEngine;

internal sealed class EntryPoint : MonoBehaviour
{
    [Header("Initial Settings")]
    [SerializeField] private float _speedCar = 15f;
    [SerializeField] private float _jump = 5;

    private const GameState InitialState = GameState.MainMenu;

    [Header("Scene Object")]
    [SerializeField] private Transform _placeForUi;

    [Header("Services")]
    [SerializeField] private UnityAdsService _adsService;
    [SerializeField] private AnalyticsManager _analyticsManager;

    private MainController _mainController;

    private void Start()
    {
        var profilePlayer = new ProfilePlayer(_speedCar, _jump, InitialState);
        _mainController = new MainController(_placeForUi, profilePlayer, _analyticsManager);

        _analyticsManager.SendMainMenuOpenedEvent();

        if (_adsService.IsInitialized)
            OnAdsInitialized();
        else _adsService.Initialized.AddListener(OnAdsInitialized);
    }

    private void OnDestroy()
    {
        _adsService.Initialized.RemoveListener(OnAdsInitialized);
        _mainController.Dispose();
    }

    private void OnAdsInitialized() =>
        _adsService.IntersititialPlayer.Play();
}
