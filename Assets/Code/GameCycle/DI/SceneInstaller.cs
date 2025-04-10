using UnityEngine;
using Zenject;

namespace Code
{
    public class SceneInstaller : MonoInstaller
    {
 
        [SerializeField] BowArrowsSystem bowArrowsSystem;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameManager>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<BowArrowsSystem>().FromInstance(bowArrowsSystem).AsSingle().NonLazy();
            const int initialPoolSize = 30;
            Container.BindMemoryPool<BowArrow, BowArrowsSystem.Pool>().WithInitialSize(initialPoolSize).FromComponentInNewPrefab(bowArrowsSystem.BowArrowDefaultPrefab).AsSingle();
            //Container.InstantiatePrefabForComponent<PlayerModel>(PlayerPrefab, PlayerSpawnPoint);
        }
    }
}