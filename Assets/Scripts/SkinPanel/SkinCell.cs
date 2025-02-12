using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace StarProject
{
    public class SkinCell : MonoBehaviour
    {
        [SerializeField] private Button _leftButton;
        [SerializeField] private Button _rightButton;
        [SerializeField] private TMP_Text _skinTypeText;
        [SerializeField] private TMP_Text _skinNameText;

        private SkinNameSO _skinName;
        private int _count = 0;
        private void Awake()
        {
            _leftButton.onClick.AddListener(ToLeft);
            _rightButton.onClick.AddListener(ToRight);
        }

        public void Init(SkinNameSO skinName)
        {
            _skinName = skinName;
            _skinNameText.text = skinName.Skin;
            InitText(_count);
        }
        
        private void ToLeft()
        {
            _count--;

            if (_count < 0)
            {
                _count = _skinName.SkinName.Length - 1;
            }
            InitText(_count);
        }
        
        private void ToRight()
        {
            _count++;
            if (_count > _skinName.SkinName.Length - 1)
            {
                _count = 0;
            }
            InitText(_count);
        }

        private void InitText(int idx)
        {
            _skinTypeText.text = _skinName.SkinName[idx];
        }
    }
}