using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace StarProject
{
    public class VisualiseUserName : MonoBehaviour
    {
        [SerializeField] private GameObject _namePanel;
        [SerializeField] private TMP_InputField _nameInputField;
        [SerializeField] private TMP_Text _textName;
        private PlayerData _playerData;
        private Database _database;

        [Inject]
        public void Construct(PlayerData playerData, Database database)
        {
            _playerData = playerData;
            _database = database;
        }
        

        private void Start()
        {
            if (_playerData.PlayerDataModel.PlayerName != null)
            {
                _textName.text = _playerData.PlayerDataModel.PlayerName;
                _namePanel.SetActive(false);
            }

            else
            {
                _namePanel.SetActive(true);
            }
        }
        
        public async void SaveButtonClicked()
        {
            _textName.text = _nameInputField.text;
            PlayerDataModel playerDataModel = new PlayerDataModel();
            playerDataModel.PlayerName = _nameInputField.text;
            await _database.SetUserData(playerDataModel);
            _namePanel.SetActive(false);
        }

        public void OpenNamePanel()
        {
            _namePanel.SetActive(true);
        }
    }
}
