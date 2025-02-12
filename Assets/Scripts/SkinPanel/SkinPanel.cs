using System;
using UnityEngine;

namespace StarProject
{
    public class SkinPanel : MonoBehaviour
    {
        [SerializeField] private SkinNameSO[] _skinNameSO;
        [SerializeField] private SkinCell _skinCellPrefab;
        [SerializeField] private Transform _content;
        
        private void Awake()
        {
            foreach (SkinNameSO skinNameSO in _skinNameSO)
            {
                SkinCell skinCell = Instantiate(_skinCellPrefab, _content);
                skinCell.Init(skinNameSO);
            }
        }
    }
}