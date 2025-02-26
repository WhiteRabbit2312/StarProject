using UnityEngine;
using Zenject;

namespace StarProject
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private PlayerSpawner _playerSpawner;
        public override void InstallBindings()
        {
            Container.BindInstance(_playerSpawner).AsSingle();
        }
    }
}