using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Code
{
    public class PlayerInstaller : MonoInstaller, IInitializable
    {
        [SerializeField] ScriptableObject inputConfig;
        [SerializeField] MonoBehaviour playerPrefab;
        [SerializeField] Transform playerSpawnPoint;

        public override void InstallBindings()
        {
            BindSystems();
            BindModel();
            BindAgents();
            BindViews();
        }

        private void BindSystems()
        {
            switch (inputConfig)
            {
                case MouseInputConfig mouseInputConfig:
                    Container.Bind<IMouseInputConfig>().FromInstance(mouseInputConfig).AsSingle().NonLazy();
                    Container.BindInterfacesTo<MouseInputManager>().AsSingle().NonLazy();
                    break;
                case KeyboardInputConfig keyboardInputConfig:
                    Container.Bind<IKeyboardInputConfig>().FromInstance(keyboardInputConfig).AsSingle().NonLazy();
                    Container.BindInterfacesTo<KeyboardInputManager>().AsSingle().NonLazy();
                    break;
            }
        }

        private void BindModel()
        {
            Container.BindInterfacesTo<PlayerInstaller>().FromInstance(this).AsSingle().NonLazy();
            //Container.InstantiatePrefabForComponent<PlayerModel>(PlayerPrefab, PlayerSpawnPoint.position, Quaternion.identity, null);
            //Container.BindInterfacesAndSelfTo<PlayerModel>().FromComponentOn(playerInstance).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerModel>().FromComponentInNewPrefab(playerPrefab).AsSingle().NonLazy();
        }

        private void BindAgents()
        {
            Container.BindInterfacesAndSelfTo<PlayerMoveAgent>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerLookAgent>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerShootAgent>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerShootAimAgent>().FromComponentsInChildren().AsSingle().NonLazy();
        }

        private void BindViews()
        {
            Container.Bind<PlayerViewPresenter>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerMoveViewLayer0>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerShootViewLayer1>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerAimViewLayer2>().AsSingle().NonLazy();
        }

        public void Initialize()
        {
            //var playerInstance = Container.InstantiatePrefab(PlayerPrefab, PlayerSpawnPoint.position, Quaternion.identity, null);
            //Container.BindInterfacesAndSelfTo<PlayerModel>().FromComponentOn(playerInstance).AsSingle().NonLazy();
            var player = Container.Resolve<PlayerModel>();
            player.transform.position = playerSpawnPoint.position;
        }
    }
}