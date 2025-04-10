using UnityEngine;
using Zenject;

namespace Code
{
    public class BowWeapon : IWeapon
    {
        BowArrowConfig _bowArrowConfig;
        BowArrowsSystem _bowArrowsSystem;

        [Inject]
        void Construct(BowArrowConfig bowArrowConfig, BowArrowsSystem bowArrowsSystem)
        {
            _bowArrowConfig = bowArrowConfig;
            _bowArrowsSystem = bowArrowsSystem;
        }

        public void FireByArgs(WeaponsController.FireArgs args)
        {
            _bowArrowsSystem.CreateBow(
                args,
                _bowArrowConfig
            );
        }

        public Vector2 PredictTrajectory(Vector2 direction, float launchForceMultiplier, float time)
        {
            var force = launchForceMultiplier * _bowArrowConfig.LaunchForce;
            var gravity = (Vector2)Physics.gravity * _bowArrowConfig.Gravity;
            var result = direction.normalized * force * time + 0.5f * gravity * time * time;

            return result;
        }
    }
}