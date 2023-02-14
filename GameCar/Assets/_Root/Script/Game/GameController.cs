using Features.AbilitySystem;
using GameCar.Car;
using GameCar.InputLogic;
using GameCar.TapeBackground;
using GameCarProfile;
using GameCarTool;
using Services.Analytics;
using UnityEngine;

namespace GameCar
{
    internal sealed class GameController : BaseController
    {
        private readonly AbilitiesContext _abilitiesControllerContext;


        public GameController(Transform placeForUi, ProfilePlayer profilePlayer, AnalyticsManager analyticsManager)
        {
            var leftMoveDiff = new SubscriptionProperty<float>();
            var rightMoveDiff = new SubscriptionProperty<float>();

            var tapeBackgroundController = new TapeBackgroundController(leftMoveDiff, rightMoveDiff);
            AddController(tapeBackgroundController);

            var inputGameController = new InputGameController(leftMoveDiff, rightMoveDiff, profilePlayer.CurrentCar);
            AddController(inputGameController);

            var carController = new CarController(profilePlayer.CurrentCar);
            AddController(carController);

            _abilitiesControllerContext = new AbilitiesContext(placeForUi, carController);
            AddContext(_abilitiesControllerContext);


            analyticsManager.SendStartGameEvent();
        }
    }
}
