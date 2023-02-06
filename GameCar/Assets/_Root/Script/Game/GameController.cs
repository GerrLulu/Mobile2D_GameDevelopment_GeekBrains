using GameCar.Car;
using GameCar.InputLogic;
using GameCar.TapeBackground;
using GameCarProfile;
using GameCarTool;
using Services.Analytics;

namespace GameCar
{
    internal sealed class GameController : BaseController
    {
        public GameController(ProfilePlayer profilePlayer, AnalyticsManager analyticsManager)
        {
            var leftMoveDiff = new SubscriptionProperty<float>();
            var rightMoveDiff = new SubscriptionProperty<float>();

            var tapeBackgroundController = new TapeBackgroundController(leftMoveDiff, rightMoveDiff);
            AddController(tapeBackgroundController);

            var inputGameController = new InputGameController(leftMoveDiff, rightMoveDiff, profilePlayer.CurrentCar);
            AddController(inputGameController);

            var carController = new CarController();
            AddController(carController);


            analyticsManager.SendStartGameEvent();
        }
    }
}
