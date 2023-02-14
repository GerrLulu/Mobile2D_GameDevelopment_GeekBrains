using GameCarTool;
using UnityEngine;

namespace GameCar.InputLogic
{
    internal sealed class GyroscopeInputView : BaseInputView
    {
        [SerializeField] private float _inputMultiplier = 10;

        public override void Init(SubscriptionProperty<float> leftMove, SubscriptionProperty<float> rightMove,
            float speed)
        {
            base.Init(leftMove, rightMove, speed);
            UnityEngine.Input.gyro.enabled = true;
        }


        protected override void Move()
        {
            if (!SystemInfo.supportsGyroscope)
                return;

            Quaternion quaternion = UnityEngine.Input.gyro.attitude;
            quaternion.Normalize();

            float offset = quaternion.x + quaternion.y;
            float moveValue = _speed * _inputMultiplier * Time.deltaTime * offset;

            float abs = Mathf.Abs(moveValue);
            float sign = Mathf.Sign(moveValue);

            if (sign > 0)
                OnRightMove(abs);
            else
                OnLeftMove(abs);
        }
    }
}
