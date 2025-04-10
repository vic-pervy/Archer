using System;
using UnityEngine;
using Zenject;

namespace Code
{
    public class PlayerLookAgent : IGameObjectActiveListener, IGameTickable
    {
        public bool enabled { get; private set; }

        IInputManager _inputManager;
        PlayerModel _playerModel;

        [Inject]
        void Construct(IInputManager inputManager, PlayerModel playerModel)
        {
            _inputManager = inputManager;
            this._playerModel = playerModel;
        }

        void IGameObjectActiveListener.OnEnable()
        {
            enabled = true;
            _inputManager.OnCursorPositionChanged += OnCursorPositionChanged;
        }

        void IGameObjectActiveListener.OnDisable()
        {
            enabled = false;
            _inputManager.OnCursorPositionChanged -= OnCursorPositionChanged;
        }


        private void OnCursorPositionChanged(Vector2 screenPosition)
        {
            //public Vector2 WorldAimPoint => PlayerCamera.ScreenToWorldPoint(transform.TransformPoint(LocalAimPoint));
            var worldAimPoint = _playerModel.PlayerCamera.ScreenToWorldPoint(screenPosition);
            var localAimPoint = _playerModel.transform.InverseTransformPoint(worldAimPoint);
            _playerModel.LocalAimPoint = localAimPoint;
        }

        void EnableAimUI(bool enable)
        {
            
        }
        
        public void GameTick()
        {
            
        }
    }
}