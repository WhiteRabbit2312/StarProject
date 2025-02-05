using UnityEngine;
using UnityEngine.SceneManagement;

namespace StarProject
{
    public class SignUpButton : AuthorizationButton
    {
        public override void AuthorizationButtonClicked()
        {
            SignUpButtonPressed();
        }
        
        private async void SignUpButtonPressed()
        {
            if (_checkAuthorization.ValidateRegistration(_inputData.Login.text, 
                    _inputData.Password.text, _inputData.ConfirmPassword.text))
            {
                await _authorization.RegistrateUserAsync(_inputData.Login.text, _inputData.Password.text).ContinueWith(task =>
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
                LoadMenuScene();
            }
            else
            {
                Debug.LogError("Checking sign up failed");
            }
        }

        private void LoadMenuScene()
        {
            SceneManager.LoadScene(1);
        }
    }
}