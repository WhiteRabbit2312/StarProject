using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;

namespace StarProject
{
    [RequireComponent(typeof(Button))]
    public class SaveNameButton : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _nameInputField;
        [SerializeField] private GameObject _nameCanvas;
        [SerializeField] private VisualiseUserName _visualiseUserName;

        private Button _button;
        private Database _database;

        [Inject]
        public void Construct(Database database)
        {
            _database = database;
        }

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(SaveButtonClicked);
        }

        private void SaveButtonClicked()
        {
            _database.SetUserData(Constants.DatabaseUserNameKey, _nameInputField.text);
            _database.SetUserData(Constants.DatabaseUserAvatarKey, 0);
            _visualiseUserName.GetUserName();
            _nameCanvas.SetActive(false);
        }
    }
}
