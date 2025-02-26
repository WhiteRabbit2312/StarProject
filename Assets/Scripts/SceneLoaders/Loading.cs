using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace StarProject
{
    [RequireComponent(typeof(Image))]
    public class Loading : MonoBehaviour
    {
        private AsyncOperation _sceneOperation;

        [SerializeField]
        private Slider _loadingSlider;

        [SerializeField]
        private Button _playButton;

        
        private async void Awake()
        {
            _playButton.onClick.AddListener(GoToNextLevel);
            await LoadNextLevelAsync("MainMenuScene");
        }

        private async Task LoadNextLevelAsync(string level)
        {
            _sceneOperation = SceneManager.LoadSceneAsync(level);
            _sceneOperation.allowSceneActivation = false;

            while (!_sceneOperation.isDone)
            {
                _loadingSlider.value = _sceneOperation.progress;

                if (_sceneOperation.progress >= 0.9f && !_playButton.gameObject.activeInHierarchy)
                    _playButton.gameObject.SetActive(true);

                await Task.Yield();
            }

            Debug.Log($"Loaded Level {level}");
        }
        
        // Function to handle which level is loaded next
        public void GoToNextLevel()
        {
            _sceneOperation.allowSceneActivation = true;
        }
    }
}
