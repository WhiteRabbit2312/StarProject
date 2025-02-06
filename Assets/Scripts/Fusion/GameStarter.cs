using UnityEngine;
using Fusion;
using UnityEngine.SceneManagement;

namespace StarProject
{
    public class GameStarter : MonoBehaviour
    {
        [SerializeField] private SceneIndexSO _sceneIndex;
        private NetworkRunner _networkRunner;
        
        public async void StartGame(GameMode mode, string sessionName)
        {
            if (_networkRunner != null)
                return;
            _networkRunner ??= gameObject.AddComponent<NetworkRunner>();
        
            _networkRunner.ProvideInput = true;

            gameObject.AddComponent<NetworkEvents>();

            var scene = SceneRef.FromIndex(_sceneIndex.LobbySceneIdx);
            var sceneInfo = new NetworkSceneInfo();

            if (scene.IsValid)
            {
                sceneInfo.AddSceneRef(scene, LoadSceneMode.Additive);
            }

            var result = await _networkRunner.StartGame(new StartGameArgs()
            {
                GameMode = mode,
                CustomLobbyName = Constants.LobbyName,
                SessionName = sessionName,
                Scene = scene,
                SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
            });

            if (!result.Ok)
            {
                Debug.LogError("result is not ok");
            }

        }
    }
}