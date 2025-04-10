using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Code
{
    public class GameCycleKernel : MonoKernel
    {
        [Inject] GameManager _gameManager;

        [Inject(Optional = true, Source = InjectSources.Local)]
        List<IGameTickable> _tickables = new();

        [Inject(Optional = true, Source = InjectSources.Local)]
        List<IGameLateTickable> _lateTickables = new();

        [Inject(Optional = true, Source = InjectSources.Local)]
        List<IGameFixedTickable> _fixedTickables = new();

        public override void Update()
        {
            base.Update();

            if (_gameManager.CurrentGameState == GameState.Play)
            {
                foreach (var tickable in _tickables)
                {
                    if (!tickable.enabled) continue;
                    tickable.GameTick();
                }
            }
        }

        public override void LateUpdate()
        {
            base.LateUpdate();

            if (_gameManager.CurrentGameState == GameState.Play)
            {
                foreach (var tickable in _lateTickables)
                {
                    if (!tickable.enabled) continue;
                    tickable.LateGameTick();
                }
            }
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (_gameManager.CurrentGameState == GameState.Play)
            {
                foreach (var tickable in _fixedTickables)
                {
                    if (!tickable.enabled) continue;
                    tickable.FixedGameTick();
                }
            }
        }
    }
}