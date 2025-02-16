using UnityEngine;
using UnityEngine.UI;
using Zenject;
using UnityEngine.SceneManagement;

namespace StarProject
{
    [RequireComponent(typeof(Button))]
    public class AuthorizationButton : MonoBehaviour,IAuthorizationButton
    {
        [SerializeField] protected InputAuthorizationData InputData;
        [SerializeField] protected SceneIndexSO SceneIndex;
        protected CheckAuthorization CheckAuth;
        protected Authorization Auth;
        private Button _button;

        [Inject]
        public void Construct(Authorization authorization, CheckAuthorization checkAuthorization)
        {
            Auth = authorization;
            CheckAuth = checkAuthorization;
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