using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code
{
    [CreateAssetMenu]
    public class KeyboardInputConfig : ScriptableObject, IKeyboardInputConfig
    {
        [SerializeField] KeyboardInputKeyCodes _keyCodes;

        public bool AimUpButton => Input.GetKey(_keyCodes.AimUpButton);
        public bool AimDownButton => Input.GetKey(_keyCodes.AimDownButton);
        public bool FireButtonDown => Input.GetKeyDown(_keyCodes.FireButton);
        public bool FireButtonUp => Input.GetKeyUp(_keyCodes.FireButton);
        public bool MoveLeftButton => Input.GetKey(_keyCodes.LeftButton);
        public bool MoveRightButton => Input.GetKey(_keyCodes.RightButton);
        public bool JumpButton => Input.GetKeyDown(_keyCodes.JumpButton);
        
        [Serializable]
        struct KeyboardInputKeyCodes
        {
            public KeyCode AimUpButton;
            public KeyCode AimDownButton;
            public KeyCode FireButton;
            public KeyCode LeftButton;
            public KeyCode RightButton;
            public KeyCode JumpButton;
        }
    }

   
}