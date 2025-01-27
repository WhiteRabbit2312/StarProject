using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using System.Collections;
using System.Collections.Generic;

namespace StarProject
{
    [RequireComponent(typeof(Button))]
    public class SingUpButton : MonoBehaviour
    {
        [SerializeField] private InputRegistratonData _inputRegistratonData;
        private CheckAuthorization _checkAuthorization;
        private Authorization _authorization;
        private Button _button;

        [Inject]
        public void Construct(Authorization authorization, CheckAuthorization checkAuthorization)
        {
            _authorization = authorization;
            _checkAuthorization = checkAuthorization;
        }
        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(SignUpButtonPressed);
        }

        private async void SignUpButtonPressed()
        {
            if (_checkAuthorization.ValidateRegistration(_inputRegistratonData.Login.text, 
                    _inputRegistratonData.Password.text, _inputRegistratonData.ConfirmPassword.text))
            {
                await _authorization.RegistrateUserAsync(_inputRegistratonData.Login.text, _inputRegistratonData.Password.text).ContinueWith(task =>
                {
                    if (task.IsCanceled)
                    {
                        Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                    }
                    if (task.IsFaulted)
                    {
                        Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                    }

                    if (task.IsCompletedSuccessfully)
                    {
                        Debug.Log("CreateUserWithEmailAndPasswordAsync completed successfully.");
                    }

                });
                
            }
            else
            {
                Debug.LogError("Checking sign up failed");
            }
        }
    }
}