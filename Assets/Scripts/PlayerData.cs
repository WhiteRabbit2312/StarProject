using UnityEngine;
using Zenject;

namespace StarProject
{
    public class PlayerData : MonoBehaviour
    {
        private PlayerDataModel _playerDataModel;
        private Database _firebaseManager;

        public PlayerDataModel PlayerDataModel{get{return _playerDataModel;} private set{_playerDataModel = value;}}
        
        [Inject]
        private void Construct(Database firebaseManager)
        {
            _firebaseManager = firebaseManager;
        }

        private void Start()
        {
            _firebaseManager.OnPlayerDataLoaded += PlayerDataLoaded;
        }

        private void PlayerDataLoaded(PlayerDataModel model)
        {
            _playerDataModel = model;
        }
        
        private void OnDisable()
        {
            _firebaseManager.OnPlayerDataLoaded -= PlayerDataLoaded;
        }
    }
}