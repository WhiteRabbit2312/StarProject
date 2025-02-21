using System;
using System.Collections.Generic;
using Fusion;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace StarProject
{
    public class ConnectedPlayers : NetworkBehaviour
    {
        [SerializeField] private NetworkObject _playerNameTemplate;
        [SerializeField] private NetworkTransform _pivot;
        [Inject] private Database _database;
        private List<string> _connectedPlayerNames = new List<string>();

        [Rpc(RpcSources.All, RpcTargets.All)]
        public async void RPC_InitTemplate(string playerUserID)
        {
            Debug.LogWarning("InitTemplate");

            try
            {
                var result = await _database.GetPlayerData(Constants.DatabaseUserNameKey, playerUserID);
                
                //string result = PlayerPrefs.GetString(Constants.DatabaseUserNameKey);
                if (_connectedPlayerNames.Contains(result.ToString()))
                {
                    return;
                }

                _connectedPlayerNames.Add(result.ToString());
                NetworkObject playerName = Runner.Spawn(_playerNameTemplate, _pivot.transform.position, Quaternion.identity);
                playerName.transform.SetParent(_pivot.transform);
                Debug.LogWarning("playerName: " + playerName);
                TMP_Text playerNameText = playerName.GetComponent<TMP_Text>();
                playerNameText.text = result.ToString();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
    }
}