using UnityEngine;

namespace StarProject
{
    public class SwitchPanels : MonoBehaviour
    {
        [SerializeField] private GameObject _panelToDisable;

        public void SwitchPanel(GameObject panelToEnable)
        {
            _panelToDisable.SetActive(false);
            panelToEnable.SetActive(true);
        }
    }
}