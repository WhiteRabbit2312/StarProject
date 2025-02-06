using System.Threading.Tasks;
using Firebase.Auth;
using Firebase.Database;
using UnityEngine;

namespace StarProject
{
    public class Database
    {
        private DatabaseReference _databaseRef;
        private FirebaseUser _firebaseUser;
        public FirebaseUser FirebaseUser { get => _firebaseUser; }
        public Database()
        {
            _databaseRef = FirebaseDatabase.DefaultInstance.RootReference;
            _firebaseUser = FirebaseAuth.DefaultInstance.CurrentUser;
        }

        public void SetUserData<T>(string key, T data)
        {
            _databaseRef.Child(Constants.DatabaseUserKey)
                .Child(_firebaseUser.UserId)
                .Child(key)
                .SetValueAsync(data);
        }

        public async Task<string> GetPlayerData(string key)
        {
            var snapshot = await _databaseRef
                .Child(Constants.DatabaseUserKey)
                .Child(_firebaseUser.UserId)
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
    }
}

