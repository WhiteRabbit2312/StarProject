using UnityEngine;
using Zenject;

namespace StarProject
{
    public class DatabaseInstaller : MonoInstaller
    {
        [SerializeField] private GameStarter _gameStarterPrefab;
        [SerializeField] private Database _databasePrefab;
        [SerializeField] private PlayerData _playerDataPrefab;
        public override void InstallBindings()
        {
            Container.BindInstance(_gameStarterPrefab).AsSingle();
            Container.BindInstance(_databasePrefab).AsSingle();
            Container.BindInstance(_playerDataPrefab).AsSingle();
        }
    }
}