using Fusion;
using TMPro;
using UnityEngine;

namespace StarProject
{
    [RequireComponent(typeof(TMP_Text))]
    public class PlayersInGameText : NetworkBehaviour
    {
        [SerializeField] private TMP_Text _playerInGameText;

        [Rpc(RpcSources.All, RpcTargets.All)]
        public void RPC_ShowPlayerConnection(int playerCount)
        {
            _playerInGameText.text = playerCount.ToString();
        }
    }
}