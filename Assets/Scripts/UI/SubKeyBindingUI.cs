using TMPro;
using UnityEngine;
using UnityEngine.UI;
using EmreBeratKR.ServiceLocator;

namespace UI
{
    public class SubKeyBindingUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text nameField;
        [SerializeField] private TMP_Text keyNameField;
        [SerializeField] private Button rebindButton;


        private KeyBindingUI m_KeyBindingUI;
        
        
        private void Awake()
        {
            m_KeyBindingUI = GetComponentInParent<KeyBindingUI>();
            
            rebindButton.onClick.AddListener(() =>
            {
                m_KeyBindingUI.SetActivePressKeyToRebindPanel(true);
                
                ServiceLocator
                    .Get<GameInput>()
                    .Rebind(GetKeyBinding(), OnRebindComplete, OnRebindCancel);
            });
        }

        private void OnEnable()
        {
            UpdateVisuals();
        }


        private void OnRebindComplete()
        {
            m_KeyBindingUI.SetActivePressKeyToRebindPanel(false);
            UpdateVisuals();
        }

        private void OnRebindCancel()
        {
            m_KeyBindingUI.SetActivePressKeyToRebindPanel(false);
        }
        
        
        private void UpdateVisuals()
        {
            var keyBinding = GetKeyBinding();
            var bindingName = ServiceLocator
                .Get<GameInput>()
                .KeyBindingToName(keyBinding);

            var keyName = ServiceLocator
                .Get<GameInput>()
                .KeyBindingToKeyName(keyBinding);

            nameField.text = bindingName;
            keyNameField.text = keyName;
        }

        private KeyBinding GetKeyBinding()
        {
            return (KeyBinding) transform.GetSiblingIndex();
        }
    }
}