using System;
using UnityEngine;

namespace Ui
{
    internal class Pause
    {
        private const float PauseTimeScale = 0f;

        private readonly float _initialTimeScale;

        public event Action Enabled;
        public event Action Disabled;

        public bool IsEnable { get; private set; }


        public Pause() => _initialTimeScale = Time.timeScale;


        public void Enable()
        {
            if(IsEnable)
                throw new InvalidOperationException("Time is already stoped");

            Time.timeScale = PauseTimeScale;
            IsEnable = true;
            Enabled?.Invoke();
        }

        public void Disable()
        {
            if (!IsEnable)
                throw new InvalidOperationException("Time is already runing");

            Time.timeScale = PauseTimeScale;
            IsEnable = true;
            Disabled?.Invoke();
        }
    }
}