using UnityEngine;

namespace StarProject
{
    public class SignUpButton : AuthorizationButton
    {
        public override void AuthorizationButtonClicked()
        {
            SignUpButtonPressed();
            LoadMenuScene();
        }
        
        private async void SignUpButtonPressed()
        {
            if (CheckAuth.ValidateRegistration(InputData.Login.text, 
                    InputData.Password.text, InputData.ConfirmPassword.text))
            {
                await Auth.RegistrateUserAsync(InputData.Login.text, InputData.Password.text).ContinueWith(task =>
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