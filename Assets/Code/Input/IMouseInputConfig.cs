using UnityEngine;

namespace Code
{
    public interface IMouseInputConfig
    {
        Vector2 CursorPosition { get; }
        bool FireButtonDown { get; }
        bool FireButtonUp { get; }
        bool MoveLeftButton { get; }
        bool MoveRightButton { get; }
        bool JumpButton { get; }
    }
}