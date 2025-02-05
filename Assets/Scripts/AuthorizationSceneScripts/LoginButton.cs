using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace StarProject
{
    public class LoginButton : AuthorizationButton
    {
        public override void AuthorizationButtonClicked()
        {
            LoginButtonPressed();
        }

        private async void LoginButtonPressed()
        {
            await _authorization.LoginUserAsync(_inputData.Login.text, _inputData.Password.text).ContinueWith(task =>
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
        
        private void LoadMenuScene()
        {
            SceneManager.LoadScene(1);
        }
    }
}