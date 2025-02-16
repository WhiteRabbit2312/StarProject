using UnityEngine;

namespace StarProject
{
    public class LoginButton : AuthorizationButton
    {
        public override void AuthorizationButtonClicked()
        {
            LoginButtonPressed();
            LoadMenuScene();
        }

        private async void LoginButtonPressed()
        {
            await Auth.LoginUserAsync(InputData.Login.text, InputData.Password.text).ContinueWith(task =>
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
    }
}