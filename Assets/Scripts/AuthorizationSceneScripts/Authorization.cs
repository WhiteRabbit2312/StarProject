using System;
using System.Collections;
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
            Debug.Log("_auth " + _auth);
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

        private async Task Login(string email, string password)
        {
            try
            {
                AuthResult result = await _auth.SignInWithEmailAndPasswordAsync(email, password);

            }
            catch (FirebaseException ex)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + ex.Message);
            }
        }
    }
}