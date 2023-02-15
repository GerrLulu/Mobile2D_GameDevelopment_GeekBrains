using GameCarUI;
using GameCar;
using GameCarProfile;
using UnityEngine;
using Services.Analytics;
using Features.Shed;

internal sealed class MainController : BaseController
{
    private readonly Transform _placeForUi;
    private readonly ProfilePlayer _profilePlayer;

    private MainMenuController _mainMenuController;
    private GameController _gameController;
    private SettingsController _settingsController;

    private ShedContext _shedContext;

    private readonly AnalyticsManager _analyticsManager;


    public MainController(Transform placeForUi, ProfilePlayer profilePlayer, AnalyticsManager analyticsManager)
    {
        _placeForUi = placeForUi;
        _profilePlayer = profilePlayer;

        _analyticsManager = analyticsManager;

        profilePlayer.CurrentState.SubscribeOnChange(OnChangeGameState);
        OnChangeGameState(_profilePlayer.CurrentState.Value);
    }

    protected override void OnDispose()
    {
        DisposeChildObjects();

        _profilePlayer.CurrentState.UnSubscribeOnChange(OnChangeGameState);
    }

    private void OnChangeGameState(GameState state)
    {
        DisposeChildObjects();

        switch (state)
        {
            case GameState.MainMenu:
                _mainMenuController = new MainMenuController(_placeForUi, _profilePlayer, _analyticsManager);
                break;
            case GameState.Game:
                _gameController = new GameController(_placeForUi, _profilePlayer, _analyticsManager);
                break;
            case GameState.Settings:
                _settingsController = new SettingsController(_placeForUi, _profilePlayer);
                break;
            case GameState.Shed:
                _shedContext = new ShedContext(_placeForUi, _profilePlayer);
                break;
        }
    }

    private void DisposeChildObjects()
    {
        _mainMenuController?.Dispose();
        _gameController?.Dispose();
        _settingsController?.Dispose();

        _shedContext?.Dispose();
    }
}
