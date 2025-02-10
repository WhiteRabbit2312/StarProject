using System;
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
            Debug.LogError("Construct");

            _gameStarter = gameStarter;
        }

        private void Awake()
        {
            Debug.LogError("Awake");
        }

        public override void Spawned()
        {
            Debug.LogError(" spawned");
            if (Runner.IsClient)
            {
                var player = Runner.Spawn(_playerPrefab, _spawnPoint, Quaternion.identity, _gameStarter.NetworkRunner.LocalPlayer);
                Rpc_AddPlayer(Runner.LocalPlayer, player);
            }
        }
        
        [Rpc(RpcSources.All, RpcTargets.StateAuthority, InvokeLocal = true)]
        private void Rpc_AddPlayer(PlayerRef playerRef, NetworkObject networkObject)
        {
            Debug.LogError("AddPlayer");
            Players.Add(playerRef, networkObject);
        }
    }
}