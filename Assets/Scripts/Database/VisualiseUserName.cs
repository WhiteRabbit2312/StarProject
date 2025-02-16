using TMPro;
using UnityEngine;
using Zenject;

namespace StarProject
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class VisualiseUserName : MonoBehaviour
    {
        private Database _database;
        private TextMeshProUGUI _text;

        [Inject]
        public void Construct(Database database)
        {
            _database = database;
            _text = gameObject.GetComponent<TextMeshProUGUI>();
        }

        public async void GetUserName()
        {
            var userName = await _database.GetPlayerData(Constants.DatabaseUserNameKey);
            _text.text = userName;
        }
    }
}
