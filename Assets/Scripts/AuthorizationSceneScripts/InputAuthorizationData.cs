using UnityEngine;
using TMPro;

namespace StarProject
{
    public class InputAuthorizationData : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _login;
        [SerializeField] private TMP_InputField _password;
        [SerializeField] private TMP_InputField _confirmPassword;
        
        public TMP_InputField Login { get { return _login; } }
        public TMP_InputField Password { get { return _password; } }
        public TMP_InputField ConfirmPassword { get { return _confirmPassword; } }

        private void Awake()
        {
#if UNITY_EDITOR
            _login.text = "login@gmail.com";
#else
            _login.text = "login3@gmail.com";
#endif
            _password.text = "123123";
        }
    }
}