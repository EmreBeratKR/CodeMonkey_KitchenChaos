using EmreBeratKR.ServiceLocator;
using TMPro;
using UnityEngine;

namespace UI
{
    public class KeyBindingDisplayUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text keyNameField;
        
        
        private void OnEnable()
        {
            UpdateVisuals();
        }


        private void UpdateVisuals()
        {
            var keyBinding = GetKeyBinding();
            
            var keyName = ServiceLocator
                .Get<GameInput>()
                .KeyBindingToKeyName(keyBinding);

            keyNameField.text = keyName;
        }
        
        private KeyBinding GetKeyBinding()
        {
            return (KeyBinding) transform.GetSiblingIndex();
        }
    }
}