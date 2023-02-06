using GameCarProfile;
using Services.Ads.UnityAds;
using Services.Analytics;
using UnityEngine;

internal sealed class EntryPoint : MonoBehaviour
{
    private const float SpeedCar = 15f;
    private const GameState InitialState = GameState.MainMenu;

    [SerializeField] private Transform _placeForUi;
    [SerializeField] private UnityAdsService _adsService;
    [SerializeField] private AnalyticsManager _analyticsManager;

    private MainController _mainController;

    private void Start()
    {
        var profilePlayer = new ProfilePlayer(SpeedCar, InitialState);
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
