using Features.Shed.Upgrade;

namespace GameCar.Car
{
    internal sealed class CarModel : IUpgradable
    {
        private readonly float _defaultSpeed;

        public float Speed { get; set; }

        public CarModel(float speed)
        {
            _defaultSpeed = speed;
            Speed = speed;
        }

        public void Restore() =>
            Speed =_defaultSpeed;
    }
}
