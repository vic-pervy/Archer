using UnityEngine;

namespace Code
{
    public interface IGameObjectActiveListener
    {
        void OnEnable();
        void OnDisable();
    }
}