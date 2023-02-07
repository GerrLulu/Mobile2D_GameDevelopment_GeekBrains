using Features.Inventory;
using GameCar.Car;
using GameCarTool;

namespace GameCarProfile
{
    internal sealed class ProfilePlayer
    {
        public readonly SubscriptionProperty<GameState> CurrentState;
        public readonly CarModel CurrentCar;
        public readonly InventoryModel Inventory;

        public ProfilePlayer(float speedCar, GameState initialState) : this(speedCar)
        {
            CurrentState.Value = initialState;
        }

        public ProfilePlayer(float speedCar)
        {
            CurrentState = new SubscriptionProperty<GameState>();
            CurrentCar = new CarModel(speedCar);
            Inventory = new InventoryModel();
        }
    }
}
