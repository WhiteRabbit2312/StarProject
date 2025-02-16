using System.Threading.Tasks;
using Firebase;
using UnityEngine;
using Firebase.Auth;

namespace StarProject
{
    public class Authorization 
    {
        private FirebaseAuth _auth;

        public Authorization()
        {
            _auth = FirebaseAuth.DefaultInstance;
        }
        
        public async Task RegistrateUserAsync(string login, string password)
        {
            if(_auth != null)
                await _auth.CreateUserWithEmailAndPasswordAsync(login, password);
            else
            {
                Debug.LogWarning("No auth found");
            }
        }

        public async Task LoginUserAsync(string login, string password)
        {
            try
            {
                AuthResult result = await _auth.SignInWithEmailAndPasswordAsync(login, password);

            }
            catch (FirebaseException ex)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + ex.Message);
            }
        }
    }
}