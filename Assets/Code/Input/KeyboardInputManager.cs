using System;
using UnityEngine;
using Zenject;

namespace Code
{
    public class KeyboardInputManager : IGameTickable, IInputManager
    {
        public event Action<Vector2> OnCursorPositionChanged;
        public event Action OnFireButtonDown;
        public event Action OnFireButtonUp;
        public event Action<float> OnMove;
        public event Action OnJump;

        IKeyboardInputConfig _keyboardInputConfig;
        Vector2 _aimPosition = Vector2.right;
        Vector2 _lastAimPosition = Vector2.negativeInfinity;
        float _lastSpeed = float.NegativeInfinity;

        [Inject]
        void Construct(IKeyboardInputConfig mouseInputConfig)
        {
            this._keyboardInputConfig = mouseInputConfig;
            _aimPosition = new Vector2(Screen.width, Screen.height / 2);
        }

        bool IGameTickable.enabled => true;

        void IGameTickable.GameTick()
        {
            FakeKeyboardAim();
            Fire();
            Jump();
            Move();
        }

        private void Jump()
        {
            if (_keyboardInputConfig.JumpButton)
            {
                OnJump?.Invoke();
            }
        }

        private void Move()
        {
            if (!_keyboardInputConfig.MoveLeftButton && !_keyboardInputConfig.MoveRightButton)
            {
                if (_lastSpeed != 0) OnMove?.Invoke(0);
                _lastSpeed = 0;
            }
            else
            {
                _lastSpeed = 0;
                if (_keyboardInputConfig.MoveLeftButton) _lastSpeed -= 1;
                if (_keyboardInputConfig.MoveRightButton) _lastSpeed += 1;
                OnMove?.Invoke(_lastSpeed);
            }
        }

        private void Fire()
        {
            if (_keyboardInputConfig.FireButtonDown)
            {
                OnFireButtonDown?.Invoke();
            }

            if (_keyboardInputConfig.FireButtonUp)
            {
                OnFireButtonUp?.Invoke();
            }
        }

        private void FakeKeyboardAim()
        {
            if (_keyboardInputConfig.AimUpButton)
            {
                _aimPosition.y += 10000 * Time.deltaTime;
            }
            if (_keyboardInputConfig.AimDownButton)
            {
                _aimPosition.y -= 10000 * Time.deltaTime;
            }

            if (_lastAimPosition == _aimPosition) return;
            
            OnCursorPositionChanged?.Invoke(_aimPosition);
            _lastAimPosition = _aimPosition;
        }
    }
}