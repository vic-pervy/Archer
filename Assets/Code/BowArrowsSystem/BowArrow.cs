using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code
{
    public class BowArrow : MonoBehaviour
    {
        public Rigidbody2D Rigidbody2D;
        public Vector3 pos;
        public float TimeToDestroy;
        public event Action<BowArrow, Collision2D> OnCollisionEntered;

        void OnCollisionEnter2D(Collision2D collision)
        {
            OnCollisionEntered?.Invoke(this, collision);
        }
    }
}