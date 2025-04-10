using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Code
{
    public class WeaponsInstaller : MonoInstaller
    {
        [SerializeField] BowArrowConfig bowArrowConfig;

        public override void InstallBindings()
        {
            Container.BindInstance(bowArrowConfig).AsSingle();
            Container.Bind<BowWeapon>().AsSingle();
            Container.BindInterfacesAndSelfTo<WeaponsController>().AsSingle();
        }
    }
}