using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Code
{
    [SelectionBase]
    public class PlayerModel : MonoBehaviour, IGameTickable
    {
        public Transform ShootTransform;
        public Camera PlayerCamera;
        public event Action ShootEvent;
        [NonSerialized] public bool FacingLeft;
        [NonSerialized] public float CurrentSpeed;
        [NonSerialized] public PlayerBodyState CurrentBodyState;
        [NonSerialized] public Vector2 LocalAimPoint = Vector2.right;
        [NonSerialized] public float LastLaunchForceMultiply = 1;

        List<IGameObjectActiveListener> _gameObjectActiveListeners = new();

        [Inject]
        void Construct(List<IGameObjectActiveListener> gameObjectActiveListeners)
        {
            _gameObjectActiveListeners = gameObjectActiveListeners ?? new List<IGameObjectActiveListener>();
        }

        void IGameTickable.GameTick()
        {
            if (CurrentSpeed != 0)
            {
                bool speedIsNegative = (CurrentSpeed < 0f);
                FacingLeft = speedIsNegative;
            }
        }

        void OnEnable()
        {
            // TODO - Camera manager
            if (!Camera.main) PlayerCamera.gameObject.SetActive(true);

            foreach (var listener in _gameObjectActiveListeners)
            {
                listener.OnEnable();
            }
        }

        private void OnDisable()
        {
            foreach (var listener in _gameObjectActiveListeners)
            {
                listener.OnDisable();
            }
        }

        public void ChangeState(PlayerBodyState state)
        {
            CurrentBodyState = state;
        }

        public void FireShootEvent()
        {
            ShootEvent?.Invoke();
        }
    }
}