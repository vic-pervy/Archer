using System;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;
using Object = UnityEngine.Object;

namespace Code
{
    public class PlayerShootAimAgent : MonoBehaviour, IGameObjectActiveListener, IPauseGameListener, IGameTickable
    {
        public int DotsCount = 10;
        public float SpaceBetweenDots = 1f;
        public SpriteRenderer DotPrefab;

        SpriteRenderer[] _dots;
        IInputManager _inputManager;
        PlayerModel _playerModel;
        WeaponsController _weaponsController;

        [Inject]
        void Construct(IInputManager inputManager, PlayerModel playerModel, WeaponsController weaponsController)
        {
            _inputManager = inputManager;
            _playerModel = playerModel;
            _weaponsController = weaponsController;
            _weaponsController.ChangeWeapon(WeaponType.Bow);
            if (DotsCount < 2) DotsCount = 2;
            _dots = new SpriteRenderer[DotsCount];
            for (int i = 0; i < DotsCount; i++)
            {
                _dots[i] = Object.Instantiate(DotPrefab, transform);
            }

            gameObject.SetActive(false);
        }

        void IGameObjectActiveListener.OnEnable()
        {
            _inputManager.OnFireButtonDown += OnFireButtonDown;
            _inputManager.OnFireButtonUp += OnFireButtonUp;
        }

        void IGameObjectActiveListener.OnDisable()
        {
            _inputManager.OnFireButtonDown -= OnFireButtonDown;
            _inputManager.OnFireButtonUp -= OnFireButtonUp;
        }

        private void OnFireButtonDown()
        {
            StartAim();
        }

        public void OnPauseGame()
        {
            StartAim();
        }

        private void OnFireButtonUp()
        {
            StopAim();
        }

        void StartAim()
        {
            _playerModel.LastLaunchForceMultiply = 0;
            gameObject.SetActive(true);
            GameTick();
        }

        void StopAim()
        {
            gameObject.SetActive(false);
        }

        public void GameTick()
        {
            _playerModel.LastLaunchForceMultiply += Time.deltaTime;
            if (_playerModel.LastLaunchForceMultiply > 1) _playerModel.LastLaunchForceMultiply = 1;

            var direction = _playerModel.ShootTransform.right;
            for (int i = 0; i < DotsCount; i++)
            {
                var time = i / (DotsCount - 1f) * SpaceBetweenDots;
                var trajectory = (Vector3)_weaponsController.PredictTrajectory(direction, _playerModel.LastLaunchForceMultiply, time);

                _dots[i].transform.position = _playerModel.ShootTransform.position + trajectory;
            }
        }
    }
}