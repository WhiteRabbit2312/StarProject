using System.Collections.Generic;
using System.Linq;
using Fusion;
using UnityEngine;
using Zenject;
using System;
using System.Collections;

namespace StarProject
{
    public class LobbyConnection : NetworkBehaviour
    {
        [SerializeField] private PlayerInformationPanel _playerInformationPanel;
        [Networked] private NetworkDictionary<PlayerRef, NetworkString<_32>> _playerUserID => default;
        [SerializeField] private AvatarSpriteSO _avatarSpriteSO;
        [SerializeField] private SceneIndexSO _sceneIndexSO;
        [SerializeField] private float _showPlayerTimer;
        
        private GameStarter _gameStarter;
        private Database _database;
        
        [Inject] public void Construct(GameStarter gameStarter, Database database)
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
            SetPlayer();

            if (count == Constants.TestPlayersCount)
            {
                ShowPlayerInformPanel();
            }
        }

        private void SetPlayer()
        {
            IEnumerable<PlayerRef> player = Runner.ActivePlayers;
            PlayerRef playerRef1 = player.FirstOrDefault();
            _playerUserID.Set(playerRef1, _database.FirebaseUser.UserId);
        }

        private async void ShowPlayerInformPanel()
        {
            foreach (var result in _playerUserID)
            {
                string name = await _database.GetPlayerData(Constants.DatabaseUserNameKey, result.Value.ToString());
                string avatarID = await _database.GetPlayerData(Constants.DatabaseUserAvatarKey, result.Value.ToString());

                _playerInformationPanel.RPC_InitPlayerPanel(name, avatarID);
            }
            StartCoroutine(LoadSceneCoroutine());
        }
        
        private IEnumerator LoadSceneCoroutine()
        {
            yield return new WaitForSeconds(_showPlayerTimer);
            RPC_LoadGameplayScene();
        }

        [Rpc(RpcSources.All, RpcTargets.All)]
        private void RPC_LoadGameplayScene()
        {
            SceneRef scene = SceneRef.FromIndex(_sceneIndexSO.GameplaySceneIdx);
            _gameStarter.NetworkRunner.LoadScene(scene);
        }
        
        /*

        [Networked] private float CountdownTimer { get; set; } = -1f;
        [SerializeField] private float _startTime;
        private IDisposable _countdownSubscription;
        
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
                    RPC_InitPlayerCells();
                })
                .AddTo(this);
        }
       */
    }
}