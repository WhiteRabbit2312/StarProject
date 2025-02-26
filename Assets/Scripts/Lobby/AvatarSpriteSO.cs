using UnityEngine;

namespace StarProject
{
    [CreateAssetMenu(fileName = "new Avatar sprite", menuName = "Avatar sprite", order = 2)]
    public class AvatarSpriteSO : ScriptableObject
    {
        public Sprite[] AvatarSprites;
    }
}