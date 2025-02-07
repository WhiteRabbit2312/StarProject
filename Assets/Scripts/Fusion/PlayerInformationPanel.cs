using System.Collections.Generic;
using System.Linq;
using Fusion;
using UnityEngine;
using Zenject;
using System;
using UniRx;


namespace StarProject
{
    public class PlayerInformationPanel : NetworkBehaviour
    {
        [SerializeField] private PlayerCell _playerCellPrefab;
        [SerializeField] private Transform _playerCellContainer;
        private Database _database;
        [SerializeField] private AvatarSpriteSO _avatarSpriteSO;
        
        public void Construct(Database database)
        {
            _database = database;
        }
        
        [Rpc(RpcSources.All, RpcTargets.All)]
        public void RPC_InitPlayerPanel(string nick, string avatarID)
        {
            gameObject.SetActive(true);
            Debug.LogWarning("RPC_InitPlayerPanel");
            if (int.TryParse(avatarID, out int id))
            {
                PlayerCell playerCell = Instantiate(_playerCellPrefab, _playerCellContainer);
                Debug.LogWarning("playerCell: " + playerCell);
 
                playerCell.AvatarImage.sprite = _avatarSpriteSO.AvatarSprites[id];
                playerCell.NicknameText.text = nick;
            }

        }
    }


}
