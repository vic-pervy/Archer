using UnityEngine;

namespace Code
{
    public interface IWeapon
    {
        void FireByArgs(WeaponsController.FireArgs args);
        Vector2 PredictTrajectory(Vector2 direction, float launchForceMultiplier, float time);
    }
}