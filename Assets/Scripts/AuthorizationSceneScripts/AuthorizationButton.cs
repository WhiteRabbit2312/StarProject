using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using UnityEngine.SceneManagement;

namespace StarProject
{
    [RequireComponent(typeof(Button))]
    public class AuthorizationButton : MonoBehaviour,IAuthorizationButton
    {
        [SerializeField] protected InputAuthorizationData _inputData;
        [SerializeField] protected SceneIndexSO SceneIndex;
        protected CheckAuthorization _checkAuthorization;
        protected Authorization _authorization;
        private Button _button;

        [Inject]
        public void Construct(Authorization authorization, CheckAuthorization checkAuthorization)
        {
            _authorization = authorization;
            _checkAuthorization = checkAuthorization;
        }
        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(AuthorizationButtonClicked);
        }

        public virtual void AuthorizationButtonClicked()
        {
            
        }
        
        protected void LoadMenuScene()
        {
            SceneManager.LoadSceneAsync(SceneIndex.LoadingSceneIdx);
        }
    }
}