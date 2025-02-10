using System.Collections.Generic;
using System.Linq;
using Fusion;
using UnityEngine;
using Zenject;
using System;
using System.Collections;
using TMPro;
using UniRx;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace StarProject
{
    public class LobbyConnection : NetworkBehaviour
    {
        [Networked] private float CountdownTimer { get; set; } = -1f;
        [SerializeField] private float _startTime;
        private IDisposable _countdownSubscription;

        [SerializeField] private PlayerInformationPanel _playerInformationPanel;
        
        [Networked] private NetworkDictionary<PlayerRef, NetworkString<_32>> _playerUserID => default;
        private GameStarter _gameStarter;
        private Database _database;
        [SerializeField] private AvatarSpriteSO _avatarSpriteSO;
        
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

            if (count == 2)
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
            yield return new WaitForSeconds(3f);
            RPC_LoadGameplayScene();
        }

        [Rpc(RpcSources.All, RpcTargets.All)]
        private void RPC_LoadGameplayScene()
        {
            SceneRef scene = SceneRef.FromIndex(4);
            _gameStarter.NetworkRunner.LoadScene(scene);
        }
        
        /*
        public override void Spawned()
        {
            InitPlayers();
        }

        private void InitPlayers()
        {
            SetPlayer();
            int count = _gameStarter.NetworkRunner.ActivePlayers.Count();
            if (count == 1)
            {
                RPC_StartCountdown();
            }
        }

        private void SetPlayer()
        {
            IEnumerable<PlayerRef> player = Runner.ActivePlayers;
            PlayerRef playerRef1 = player.FirstOrDefault();
            Debug.LogWarning("Player 1 connected: " + playerRef1);
            _playerUserID.Set(playerRef1, _database.FirebaseUser.UserId);
        }
        
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
        
        private async void RPC_InitPlayerCells()
        {
            _playerInformationPanel.gameObject.SetActive(true);
            foreach (var result in _playerUserID)
            {
                Debug.LogError("_playerUserID: " + _playerUserID.Count);
                string name = await _database.GetPlayerData(Constants.DatabaseUserNameKey, result.Value.ToString());
                string avatarID = await _database.GetPlayerData(Constants.DatabaseUserAvatarKey, result.Value.ToString());

                _playerInformationPanel.RPC_InitPlayerPanel(name, avatarID);
            }
        }*/
    }
}