using UnityEngine;

namespace Code
{
    public interface IKeyboardInputConfig
    {
        bool AimUpButton { get; }
        bool AimDownButton { get; }
        bool FireButtonDown { get; }
        bool FireButtonUp { get; }
        bool MoveLeftButton { get; }
        bool MoveRightButton { get; }
        bool JumpButton { get; }
    }
}

