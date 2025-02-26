using Fusion;
using UnityEngine;

namespace StarProject
{
    public class InputHandler : NetworkBehaviour
    {
        private IInput _input;

        public void Init(IInput input)
        {
            _input = input;
        }
        
        public override void Spawned()
        {
            if (Runner.IsClient)
            {
                Runner.GetComponent<NetworkEvents>().OnInput.AddListener(OnInputProvide);
            }
        }

        public void OnInputProvide(NetworkRunner runner, NetworkInput input)
        {
            var data = new PlayerInput();
            input.Set(data);
        }
    }
}