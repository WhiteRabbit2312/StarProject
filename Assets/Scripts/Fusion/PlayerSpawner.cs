using Fusion;
using UnityEngine;
using Zenject;

namespace StarProject
{
    public class PlayerSpawner : NetworkBehaviour
    {
        [SerializeField] private NetworkObject _playerPrefab;
        [SerializeField] private Vector3 _spawnPoint;
        [Networked]
        public NetworkDictionary<PlayerRef, NetworkObject> Players => default;

        private GameStarter _gameStarter;
        [Inject]
        public void Construct(GameStarter gameStarter)
        {
            _gameStarter = gameStarter;
        }

        public override void Spawned()
        {
            if (Runner.IsClient)
            {
                var player = Runner.Spawn(_playerPrefab, _spawnPoint, Quaternion.identity, _gameStarter.NetworkRunner.LocalPlayer);
                Rpc_AddPlayer(Runner.LocalPlayer, player);
            }
        }
        
        [Rpc(RpcSources.All, RpcTargets.StateAuthority, InvokeLocal = true)]
        private void Rpc_AddPlayer(PlayerRef playerRef, NetworkObject networkObject)
        {
            Players.Add(playerRef, networkObject);
        }
    }
}