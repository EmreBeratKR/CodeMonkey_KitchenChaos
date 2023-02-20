using UnityEngine;

namespace UI
{
    public class KeyBindingUI : MonoBehaviour
    {
        [SerializeField] private GameObject pressKeyToRebindPanel;


        public void SetActivePressKeyToRebindPanel(bool value)
        {
            pressKeyToRebindPanel.SetActive(value);
        }
    }
}