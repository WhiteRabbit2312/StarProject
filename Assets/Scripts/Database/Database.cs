using System;
using System.Threading.Tasks;
using Firebase.Auth;
using Firebase.Database;
using UnityEngine;

namespace StarProject
{
    public class Database : MonoBehaviour
    {
        public event Action<PlayerDataModel> OnPlayerDataLoaded;
        
        private DatabaseReference _databaseRef;
        private FirebaseAuth _auth;
        private string _userId;
        
        private void Awake()
        {
            _databaseRef = FirebaseDatabase.DefaultInstance.RootReference;
            _auth = FirebaseAuth.DefaultInstance;
        }
        
        public async Task SetUserData(PlayerDataModel playerData)
        {
            if (string.IsNullOrEmpty(playerData.PlayerName))
            {
                Debug.LogError("[Firease Manager] Player Name is null or empty");
                return;
            }

            string json = JsonUtility.ToJson(playerData);

            await _databaseRef.Child(Constants.DatabaseUserKey).Child(_auth.CurrentUser.UserId).SetRawJsonValueAsync(json);
        }

        public async Task GetPlayerData()
        {
            var snapshot = await _databaseRef.Child(Constants.DatabaseUserKey).Child(_auth.CurrentUser.UserId).GetValueAsync();

            if (snapshot.Exists)
            {
                PlayerDataModel playerData = JsonUtility.FromJson<PlayerDataModel>(snapshot.GetRawJsonValue());
                OnPlayerDataLoaded?.Invoke(playerData);
            }
            
            else
            {
                Debug.LogError("[Firebase Manager] No player data found. Creating new player profile.");
                PlayerDataModel newPlayerData = CreateNewPlayerData();
                await SetUserData(newPlayerData);
                OnPlayerDataLoaded?.Invoke(newPlayerData);
            }
        }
        
        private PlayerDataModel CreateNewPlayerData()
        {
            PlayerDataModel newPlayerData = new PlayerDataModel();
            newPlayerData.PlayerName = "";
            return newPlayerData;
        }
        
        /*
        public async Task<string> GetPlayerData(string key, string userId)
        {
            try
            {
                var snapshot = await _databaseRef
                    .Child(Constants.DatabaseUserKey)
                    .Child(userId)
                    .Child(key)
                    .GetValueAsync();

                if (snapshot.Exists)
                {
                    string data = snapshot.Value.ToString();
                    return data;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }

            return null;
        }*/
    }
}

