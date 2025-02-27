using UnityEngine;
using Zenject;

namespace StarProject
{
    public class PlayerData : MonoBehaviour
    {
        private PlayerDataModel _playerDataModel;
        private Database _firebaseManager;

        public PlayerDataModel PlayerDataModel{get{return _playerDataModel;}}
        
        [Inject]
        private void Construct(Database firebaseManager)
        {
            _firebaseManager = firebaseManager;
        }

        private void Awake()
        {
            _firebaseManager.OnPlayerDataLoaded += PlayerDataLoaded;
        }

        public void PlayerDataLoaded(PlayerDataModel model)
        {
            _playerDataModel = model;
            Debug.LogWarning("player name in PlayerDataLoaded: " + _playerDataModel.PlayerName);
        }
        
        private void OnDisable()
        {
            _firebaseManager.OnPlayerDataLoaded -= PlayerDataLoaded;
        }
    }
}