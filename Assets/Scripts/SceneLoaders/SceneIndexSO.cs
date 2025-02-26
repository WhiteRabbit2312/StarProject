using UnityEngine;

namespace StarProject
{
    [CreateAssetMenu(fileName = "SceneIndexSO", menuName = "SceneIndexSO", order = 1)]
    public class SceneIndexSO : ScriptableObject
    {
        public int AuthSceneIdx = 0;
        public int LoadingSceneIdx = 1;
        public int MainMenuSceneIdx = 2;
        public int LobbySceneIdx = 3;
        public int EditCharacterSceneIdx = 5;
        public int GameplaySceneIdx = 4;
    }
}