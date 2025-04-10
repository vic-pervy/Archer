using System;
using UnityEngine;
using Zenject;

namespace Code
{
    public class PlayerShootAgent : IGameObjectActiveListener
    {
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
        }

        void IGameObjectActiveListener.OnEnable()
        {
            _inputManager.OnFireButtonUp += OnFireButtonUp;
        }

        void IGameObjectActiveListener.OnDisable()
        {
            _inputManager.OnFireButtonUp -= OnFireButtonUp;
        }

        private void OnFireButtonUp()
        {
            _playerModel.FireShootEvent();
            _weaponsController.FireByArgs(new WeaponsController.FireArgs()
            {
                Position = _playerModel.ShootTransform.position,
                Rotation = _playerModel.ShootTransform.rotation,
                LaunchForceMultiply = _playerModel.LastLaunchForceMultiply
            });
        }
    }
}