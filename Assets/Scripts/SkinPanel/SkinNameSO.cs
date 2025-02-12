using UnityEngine;

namespace StarProject
{
    [CreateAssetMenu(fileName = "NewSkin", menuName = "Name Skin", order = 1)]
    public class SkinNameSO : ScriptableObject
    {
        public string[] SkinName;
        public string Skin;
    }
}