using JoostenProductions;
using UnityEngine;

namespace GameCar.InputLogic
{
    internal sealed class InputKeyboard : BaseInputView
    {
        [SerializeField] private float _input = 0.01f;

        private void Start() =>
            UpdateManager.SubscribeToUpdate(Move);

        private void OnDestroy() =>
            UpdateManager.UnsubscribeFromUpdate(Move);

        private void Move()
        {
            float moveValue = _speed  * Time.deltaTime * _input;

            if (Input.GetKey(KeyCode.RightArrow))
                OnRightMove(moveValue);

            if(Input.GetKey(KeyCode.LeftArrow))
                OnLeftMove(moveValue);
        }
    }
}