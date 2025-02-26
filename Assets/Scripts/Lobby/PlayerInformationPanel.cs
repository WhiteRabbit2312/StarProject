using Fusion;
using UnityEngine;

namespace StarProject
{
    public class PlayerInformationPanel : NetworkBehaviour
    {
        [SerializeField] private PlayerCell _playerCellPrefab;
        [SerializeField] private Transform _playerCellContainer;
        [SerializeField] private AvatarSpriteSO _avatarSpriteSO;
        
        [Rpc(RpcSources.All, RpcTargets.All)]
        public void RPC_InitPlayerPanel(string nick, string avatarID)
        {
            gameObject.SetActive(true);
            if (int.TryParse(avatarID, out int id))
            {
                PlayerCell playerCell = Instantiate(_playerCellPrefab, _playerCellContainer);
 
                playerCell.AvatarImage.sprite = _avatarSpriteSO.AvatarSprites[id];
                playerCell.NicknameText.text = nick;
            }

        }
    }


}
