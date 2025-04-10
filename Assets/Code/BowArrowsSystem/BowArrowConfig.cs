using UnityEngine;
using UnityEngine.Serialization;

namespace Code
{
    [CreateAssetMenu]
    public class BowArrowConfig : ScriptableObject
    {
        public float LaunchForce = 10f;
        public float Gravity = 1f;
        public float TimeToDestroy = 3f;
    }
}