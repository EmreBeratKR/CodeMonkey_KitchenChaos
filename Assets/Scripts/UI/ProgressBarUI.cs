using General;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ProgressBarUI : MonoBehaviour
    {
        [SerializeField] private GameObject progressProvider;
        [SerializeField] private Image fill;
        [SerializeField] private bool hideWhenEmpty;


        private IProgressProvider m_ProgressProvider;


        private void Awake()
        {
            m_ProgressProvider = progressProvider.GetComponent<IProgressProvider>();

            if (m_ProgressProvider == null)
            {
                Debug.LogError($"{progressProvider} does not implement {nameof(IProgressProvider)}!");
            }
        
            m_ProgressProvider.OnProgressChanged += OnProgressChanged;

            if (hideWhenEmpty)
            {
                Hide();
            }
        }

        private void OnDestroy()
        {
            m_ProgressProvider.OnProgressChanged -= OnProgressChanged;
        }
    
    
        private void OnProgressChanged(ProgressChangedArgs args)
        {
            fill.fillAmount = args.progressNormalized;

            if (!hideWhenEmpty) return;
        
            if (args.progressNormalized > 0) Show();
            
            else Hide();
        }


        private void Show()
        {
            gameObject.SetActive(true);
        }
    
        private void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}