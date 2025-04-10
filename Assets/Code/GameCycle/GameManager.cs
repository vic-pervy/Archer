using System;
using System.Collections.Generic;
using Code.States;
using UnityEngine;
using Zenject;

namespace Code
{
    public enum GameState
    {
        Pause = 0,
        Play = 1
    }

    public class GameManager : IInitializable, IDisposable
    {
        public GameState? CurrentGameState { get; private set; }

        Dictionary<GameState, BaseGameState> _gameStates;
        List<IGameStateListeners> _gameStateListeners;

        [Inject]
        void Construct(List<IGameStateListeners> gameStateListeners)
        {
            _gameStateListeners = gameStateListeners;
            _gameStates = new Dictionary<GameState, BaseGameState>()
            {
                { GameState.Pause, new PauseGameState() },
                { GameState.Play, new PlayGameState() },
            };
        }

        public void Initialize()
        {
            ChangeState(GameState.Play);
        }

        public void Dispose()
        {
        }

        void ChangeState(GameState state)
        {
            if (CurrentGameState.HasValue) _gameStates[CurrentGameState.Value]?.ExitState();
            CurrentGameState = state;
            _gameStates[state].EnterState();
        }

        public void StartGame()
        {
            if (CurrentGameState?.GetType() == typeof(PlayGameState)) return;
            Time.timeScale = 1;
            ChangeState(GameState.Play);
            foreach (var it in _gameStateListeners)
            {
                if (it is IStartGameListener listener)
                {
                    listener.OnStartGame();
                }
            }
        }

        public void StopGame()
        {
            if (CurrentGameState?.GetType() == typeof(PauseGameState)) return;
            Time.timeScale = 0;
            ChangeState(GameState.Pause);
            foreach (var it in _gameStateListeners)
            {
                if (it is IPauseGameListener listener)
                {
                    listener.OnPauseGame();
                }
            }
        }
    }
}