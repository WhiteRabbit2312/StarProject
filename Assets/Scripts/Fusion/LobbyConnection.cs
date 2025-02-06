using System.Collections.Generic;
using System.Linq;
using Fusion;
using UnityEngine;
using Zenject;
using System;
using UniRx;

namespace StarProject
{
    public class LobbyConnection : NetworkBehaviour
    {
        [Networked] private float CountdownTimer { get; set; } = -1f;
        [SerializeField] private float _startTime;
        private IDisposable _countdownSubscription;

        [Networked]
        private NetworkDictionary<PlayerRef, NetworkString<_32>> _playerUserID => default;
        private GameStarter _gameStarter;
        private Database _database;
        [Inject]
        public void Construct(GameStarter gameStarter, Database database)
        {
            _gameStarter = gameStarter;
            _database = database;
        }
        
        public override void Spawned()
        {
            InitPlayers();
        }

        private void InitPlayers()
        {
            
            int count = _gameStarter.NetworkRunner.ActivePlayers.Count();
            Debug.LogWarning("InitPlayers");

            if (count == 1)
            {
                Debug.LogWarning("Player 1 connected");
                SetPlayer();
                RPC_StartCountdown();
            }
        }

        private void SetPlayer()
        {
            IEnumerable<PlayerRef> player = Runner.ActivePlayers;
            PlayerRef playerRef1 = player.FirstOrDefault();

            Debug.LogWarning("player ref: " + playerRef1);

            _playerUserID.Set(playerRef1, _database.FirebaseUser.UserId);
        }

        [Rpc(RpcSources.All, RpcTargets.All)]
        public void RPC_StartCountdown()
        {
            CountdownTimer = _startTime;
            Observable
                .Interval(TimeSpan.FromSeconds(1))
                .TakeWhile(_ => CountdownTimer > 0)
                .Subscribe(_ =>
                {
                    CountdownTimer -= 1;
                    Debug.Log($"time left: {CountdownTimer}");
                }, () =>
                {
                    Debug.Log("Time is up!");
                    //StartGame();
                })
                .AddTo(this);
        }
    }
}