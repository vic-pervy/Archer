using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Code
{
    public class GameCycleInstaller : MonoInstaller
    {
        [SerializeField] BowArrowsSystem bowArrowsSystem;
        
        public override void InstallBindings()
        {
            /*
            Container.BindInterfacesAndSelfTo<BowArrowsSystem>().FromInstance(bowArrowsSystem).AsSingle().NonLazy();

            const int initialPoolSize = 30;
            Container.BindMemoryPool<BowArrow, BowArrowsSystem.Pool>().WithInitialSize(initialPoolSize).FromComponentInNewPrefab(bowArrowsSystem.BowArrowDefaultPrefab).AsSingle();
        */
        }
    }
}