using System;
using UnityEngine;

namespace Code
{
    public interface IInputManager
    {
        event Action<Vector2> OnCursorPositionChanged;
        event Action OnFireButtonDown;
        event Action OnFireButtonUp;
        event Action<float> OnMove;
        event Action OnJump;
    }
}