using UnityEngine;
using Zenject;

namespace StarProject
{
    public class DatabaseInstaller : MonoInstaller
    {
        [SerializeField] private GameStarter _databasePrefab;
        public override void InstallBindings()
        {
            Container.Bind<Database>().AsSingle();
            Container.BindInstance(_databasePrefab).AsSingle();
        }
    }
}