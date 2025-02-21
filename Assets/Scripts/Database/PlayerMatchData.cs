using System;
using TMPro;
using UnityEngine;
using Zenject;

namespace StarProject
{
    [RequireComponent(typeof(TMP_Text))]
    public class PlayerMatchData : MonoBehaviour
    {
        [SerializeField] private string _key;
        private TMP_Text _damagePointsText;
        private Database _database;
        
        [Inject]
        public void Construct(Database database)
        {
            _database = database;
            _damagePointsText = GetComponent<TMP_Text>();
        }
        private void OnEnable()
        {
            var damagePoints = _database.GetPlayerData(_key, _database.FirebaseUser.UserId);
            _damagePointsText.text = Convert.ToInt32(damagePoints).ToString();
        }
        
        public async void SaveMatchData(string key, int data)
        {
            var result = await _database.GetPlayerData(key, _database.FirebaseUser.UserId);
            int totalDamage = Convert.ToInt32(result) + data;
            _database.SetUserData(Constants.DamageKey, totalDamage);
        }
    }
}