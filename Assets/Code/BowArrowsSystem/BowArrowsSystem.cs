using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Code
{
    public class BowArrowsSystem : MonoBehaviour, IFixedTickable
    {
        public class Pool : MemoryPool<BowArrow>
        {
            public readonly HashSet<BowArrow> ActiveArrows = new();
            Transform _poolContainer;

            protected override void OnCreated(BowArrow item)
            {
                base.OnCreated(item);
                if (!_poolContainer)
                {
                    _poolContainer = new GameObject("BowSystem_poolContainer").transform;
                    _poolContainer.gameObject.SetActive(false);
                }

                item.transform.SetParent(_poolContainer);
            }

            protected override void OnSpawned(BowArrow item)
            {
                base.OnSpawned(item);
                ActiveArrows.Add(item);
                item.transform.SetParent(null);
            }

            protected override void OnDespawned(BowArrow item)
            {
                base.OnDespawned(item);
                item.transform.SetParent(_poolContainer);
                ActiveArrows.Remove(item);
            }
        }

        public ParticleSystem DestroyParticles;
        public BowArrow BowArrowDefaultPrefab;

        BowArrowsSystem.Pool _pool;
        BowArrow[] _tempBowArrows = Array.Empty<BowArrow>();

        [Inject]
        void Construct(BowArrowsSystem.Pool pool)
        {
            _pool = pool;
        }

        void IFixedTickable.FixedTick()
        {
            if (_tempBowArrows.Length < _pool.ActiveArrows.Count) Array.Resize(ref _tempBowArrows, _pool.ActiveArrows.Count);

            var i = 0;
            foreach (var bowArrow in _pool.ActiveArrows)
            {
                _tempBowArrows[i++] = bowArrow;
            }

            for (i = 0; i < _pool.ActiveArrows.Count; i++)
            {
                var bowArrow = _tempBowArrows[i];
                if (bowArrow.Rigidbody2D.bodyType != RigidbodyType2D.Dynamic || !bowArrow.Rigidbody2D.simulated)
                {
                    bowArrow.TimeToDestroy -= Time.fixedDeltaTime;
                    if (bowArrow.TimeToDestroy <= 0)
                    {
                        DestroyBowArrow(bowArrow);
                    }
                }
                else
                {
                    var angle = Mathf.Atan2(bowArrow.Rigidbody2D.linearVelocity.y, bowArrow.Rigidbody2D.linearVelocity.x) * Mathf.Rad2Deg;
                    bowArrow.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                }
            }
        }


        public void CreateBow(WeaponsController.FireArgs args, BowArrowConfig bowArrowConfig)
        {
            var bowArrow = _pool.Spawn();
            bowArrow.transform.SetParent(null);
            bowArrow.transform.SetPositionAndRotation(args.Position, args.Rotation);
            bowArrow.TimeToDestroy = bowArrowConfig.TimeToDestroy;
            bowArrow.Rigidbody2D.gravityScale = bowArrowConfig.Gravity;
            bowArrow.Rigidbody2D.linearVelocity = args.LaunchForceMultiply * bowArrowConfig.LaunchForce * bowArrow.Rigidbody2D.transform.right;
            bowArrow.Rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
            bowArrow.Rigidbody2D.simulated = true;
            bowArrow.OnCollisionEntered += BowArrowOnOnCollisionEntered;
        }

        private void BowArrowOnOnCollisionEntered(BowArrow arrow, Collision2D collision)
        {
            arrow.Rigidbody2D.linearVelocity = Vector2.zero;
            arrow.Rigidbody2D.angularVelocity = 0;
            arrow.Rigidbody2D.simulated = false;
        }

        public void DestroyBowArrow(BowArrow bowArrow)
        {
            bowArrow.OnCollisionEntered -= BowArrowOnOnCollisionEntered;
            DestroyParticles.Stop();
            DestroyParticles.transform.position = bowArrow.transform.position;
            DestroyParticles.Play();
            _pool.Despawn(bowArrow);
        }
    }
}