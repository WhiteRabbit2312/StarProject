using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace StarProject
{
    [RequireComponent(typeof(Button))]
    public class ConnectionButton : MonoBehaviour
    {
        private Button _startButton;
        private GameStarter _gameStarter;

        [Inject]
        public void Construct(GameStarter gameStarter)
        {
            _gameStarter = gameStarter;
        }
        
        private void Awake()
        {
            _startButton = GetComponent<Button>();
            _startButton.onClick.AddListener(OnStartGameButtonPressed);
        }
        
        private void OnStartGameButtonPressed()
        {
            _gameStarter.StartGame(Fusion.GameMode.Shared, Constants.SessionName);
        }
    }
}