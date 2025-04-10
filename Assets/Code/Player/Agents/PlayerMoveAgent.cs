using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Code
{
    public class PlayerMoveAgent : IGameObjectActiveListener, IGameTickable
    {
        public bool enabled { get; private set; }

        IInputManager _inputManager;
        PlayerModel _playerModel;
        IEnumerator<object> jumpRoutine;

        [Inject]
        void Construct(IInputManager inputManager, PlayerModel playerModel)
        {
            _inputManager = inputManager;
            _playerModel = playerModel;
        }

        void IGameObjectActiveListener.OnEnable()
        {
            _inputManager.OnMove += OnMove;
            _inputManager.OnJump += OnJump;
            enabled = true;
        }

        void IGameObjectActiveListener.OnDisable()
        {
            _inputManager.OnMove -= OnMove;
            _inputManager.OnJump -= OnJump;
            enabled = false;
        }

        void IGameTickable.GameTick()
        {
            if (jumpRoutine == null) return;
            if (!jumpRoutine.MoveNext())
            {
                jumpRoutine = null;
            }
        }

        void OnMove(float speed)
        {
            if (_playerModel.CurrentBodyState == PlayerBodyState.Jumping) return;

            _playerModel.CurrentSpeed = speed;

            if (speed == 0)
            {
                _playerModel.ChangeState(PlayerBodyState.Idle);
            }
            else
            {
                _playerModel.ChangeState(PlayerBodyState.Idle);

                const float LEFT_BORDER = -9f;
                const float RIGHT_BORDER = -3.2f;
                const float MOVE_POWER = 5f;

                var pos = _playerModel.transform.position;
                var oldPos = pos;
                pos.x = Mathf.Clamp(pos.x + (MOVE_POWER * Time.deltaTime) * speed, LEFT_BORDER, RIGHT_BORDER);
                if (oldPos.x == pos.x)
                {
                    _playerModel.ChangeState(PlayerBodyState.Idle);
                }
                else
                {
                    _playerModel.ChangeState(PlayerBodyState.Running);
                    _playerModel.transform.position = pos;
                }
            }
        }

        void OnJump()
        {
            if (_playerModel.CurrentBodyState == PlayerBodyState.Jumping) return;
            _playerModel.CurrentBodyState = PlayerBodyState.Jumping;
            _playerModel.CurrentSpeed = 0;
            jumpRoutine = FakeJumpCoroutine().GetEnumerator();
        }

        IEnumerable<object> FakeJumpCoroutine()
        {
            Vector3 startPosition = _playerModel.transform.localPosition;
            const float JUMP_TIME = 1.0f;
            const float JUMP_POWER = 2f;
            for (float t = 0; t < JUMP_TIME; t += Time.deltaTime)
            {
                float normalizedTime = t / JUMP_TIME;
                var pos = startPosition;
                pos.y += Mathf.Sin(normalizedTime * Mathf.PI) * JUMP_POWER;
                _playerModel.transform.localPosition = pos;
                yield return null;
            }

            _playerModel.transform.localPosition = startPosition;

            _playerModel.CurrentBodyState = PlayerBodyState.Idle;
        }
    }
}