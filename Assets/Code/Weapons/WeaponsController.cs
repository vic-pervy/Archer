using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Code
{
    public class WeaponsController
    {
        public WeaponType CurrentWeapon { get; private set; }

        private Dictionary<WeaponType, IWeapon> _weapons;

        [Inject]
        void Construct(BowWeapon bowWeapon)
        {
            _weapons = new Dictionary<WeaponType, IWeapon>()
            {
                { WeaponType.Bow, bowWeapon }
            };
        }

        public void ChangeWeapon(WeaponType newWeaponType)
        {
            if (newWeaponType == CurrentWeapon) return;
            CurrentWeapon = newWeaponType;
        }

        public void FireByArgs(FireArgs args)
        {
            _weapons[CurrentWeapon].FireByArgs(args);
        }

        public Vector2 PredictTrajectory(Vector2 direction, float launchForceMultiplier, float time)
        {
            return _weapons[CurrentWeapon].PredictTrajectory(direction, launchForceMultiplier, time);
        }

        public struct FireArgs
        {
            public Vector3 Position;
            public Quaternion Rotation;
            public float LaunchForceMultiply;
        }
    }
}