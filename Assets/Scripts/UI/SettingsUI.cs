using EmreBeratKR.ServiceLocator;
using UnityEngine;

namespace UI
{
    public class SettingsUI : MonoBehaviour, IMenu
    {
        [SerializeField] private GameObject main;


        public void Open()
        {
            main.SetActive(true);
            
            ServiceLocator
                .Get<UIManager>()
                .PushMenu(this);
        }

        public void Close()
        {
            main.SetActive(false);
        }
    }
}