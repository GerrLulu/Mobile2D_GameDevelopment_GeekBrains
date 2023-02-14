using GameCarTool;
using JoostenProductions;
using UnityEngine;

namespace GameCar.InputLogic
{
    internal abstract class BaseInputView : MonoBehaviour
    {
        private SubscriptionProperty<float> _leftMove;
        private SubscriptionProperty<float> _rightMove;
        protected float _speed;


        private void Start() =>
            UpdateManager.SubscribeToUpdate(Move);

        private void OnDestroy() =>
            UpdateManager.UnsubscribeFromUpdate(Move);


        public virtual void Init(SubscriptionProperty<float> leftMove, SubscriptionProperty<float> rightMove,
            float speed)
        {
            _leftMove = leftMove;
            _rightMove = rightMove;
            _speed = speed;
        }

        
        protected abstract void Move();

        protected void OnLeftMove(float value) =>
            _leftMove.Value = value;

        protected void OnRightMove(float value) =>
            _rightMove.Value = value;
    }
}
