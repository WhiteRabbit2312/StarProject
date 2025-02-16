using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace StarProject
{
    public class SceneLoader
    {
        [SerializeField] private string _sceneAddress;

        public void LoadScene()
        {
            Addressables.LoadSceneAsync(_sceneAddress, LoadSceneMode.Single).Completed += OnSceneLoaded;
        }

        private void OnSceneLoaded(AsyncOperationHandle<SceneInstance> obj)
        {
            if (obj.Status == AsyncOperationStatus.Succeeded)
            {
                Debug.Log("Scene loaded: " + _sceneAddress);
            }
            else
            {
                Debug.LogError("Error: " + obj.OperationException);
            }
        }

        public void UnloadScene(SceneInstance sceneInstance)
        {
            
            Addressables.UnloadSceneAsync(sceneInstance).Completed += (handle) =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    Debug.Log("Scene loaded.");
                }
            };
        }
    }
}
