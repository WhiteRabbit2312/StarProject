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
        private async void OnEnable()
        {
            //var damagePoints = await _database.GetPlayerData(_key, _database.FirebaseUser.UserId);
            //_damagePointsText.text = Convert.ToInt32(damagePoints).ToString();
        }
        
        public async void SaveMatchData(string key, int data)
        {
           
        }
    }
}