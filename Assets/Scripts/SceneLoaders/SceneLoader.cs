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
            // Загружаем сцену асинхронно
            Addressables.LoadSceneAsync(_sceneAddress, LoadSceneMode.Single).Completed += OnSceneLoaded;
        }

        private void OnSceneLoaded(AsyncOperationHandle<SceneInstance> obj)
        {
            if (obj.Status == AsyncOperationStatus.Succeeded)
            {
                Debug.Log("Сцена успешно загружена: " + _sceneAddress);
            }
            else
            {
                Debug.LogError("Ошибка загрузки сцены: " + obj.OperationException);
            }
        }

        public void UnloadScene(SceneInstance sceneInstance)
        {
            
            Addressables.UnloadSceneAsync(sceneInstance).Completed += (handle) =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    Debug.Log("Сцена успешно выгружена.");
                }
            };
        }
    }
}
