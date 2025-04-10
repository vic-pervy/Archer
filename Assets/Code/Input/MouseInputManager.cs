using System;
using UnityEngine;
using Zenject;

namespace Code
{
    public sealed class MouseInputManager : IGameTickable, IInputManager
    {
        public event Action<Vector2> OnCursorPositionChanged;
        public event Action OnFireButtonDown;
        public event Action OnFireButtonUp;
        public event Action<float> OnMove;
        public event Action OnJump;

        IMouseInputConfig _mouseInputConfig;
        Vector2 _lastCursorPosition = Vector2.negativeInfinity;
        float _lastSpeed = float.NegativeInfinity;

        [Inject]
        void Construct(IMouseInputConfig mouseInputConfig)
        {
            this._mouseInputConfig = mouseInputConfig;
        }

        bool IGameTickable.enabled => true;

        void IGameTickable.GameTick()
        {
            Aim();
            Fire();
            Jump();
            Move();
        }

        private void Jump()
        {
            if (_mouseInputConfig.JumpButton)
            {
                OnJump?.Invoke();
            }
        }

        private void Move()
        {
            if (!_mouseInputConfig.MoveLeftButton && !_mouseInputConfig.MoveRightButton)
            {
                if (_lastSpeed != 0) OnMove?.Invoke(0);
                _lastSpeed = 0;
            }
            else
            {
                _lastSpeed = 0;
                if (_mouseInputConfig.MoveLeftButton) _lastSpeed -= 1;
                if (_mouseInputConfig.MoveRightButton) _lastSpeed += 1;
                OnMove?.Invoke(_lastSpeed);
            }
        }

        private void Fire()
        {
            if (_mouseInputConfig.FireButtonDown)
            {
                OnFireButtonDown?.Invoke();
            }

            if (_mouseInputConfig.FireButtonUp)
            {
                OnFireButtonUp?.Invoke();
            }
        }

        private void Aim()
        {
            if (_lastCursorPosition != _mouseInputConfig.CursorPosition)
            {
                OnCursorPositionChanged?.Invoke(_mouseInputConfig.CursorPosition);
                _lastCursorPosition = _mouseInputConfig.CursorPosition;
            }
        }
    }
}