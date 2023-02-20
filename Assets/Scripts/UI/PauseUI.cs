using EmreBeratKR.ServiceLocator;
using UnityEngine;

namespace UI
{
    public class PauseUI : MonoBehaviour, IMenu
    {
        [SerializeField] private SettingsUI settings;
        [SerializeField] private GameObject main;


        private void Awake()
        {
            GameManager.OnPaused += OnGamePaused;
            GameManager.OnUnPaused += OnGameUnPaused;
        }

        private void OnDestroy()
        {
            GameManager.OnPaused -= OnGamePaused;
            GameManager.OnUnPaused -= OnGameUnPaused;
        }

        private void OnGamePaused()
        {
            Open();
        }
        
        private void OnGameUnPaused()
        {
            Close();
        }


        public void OnClickedResume()
        {
            ServiceLocator
                .Get<GameManager>()
                .Resume();
        }

        public void OnClickedRestart()
        {
            ServiceLocator
                .Get<GameManager>()
                .Resume();
            
            ServiceLocator
                .Get<SceneLoader>()
                .LoadScene(Scene.Game);
        }

        public void OnClickedSettings()
        {
            if (!settings) return;
            
            settings.Open();
        }

        public void OnClickedMainMenu()
        {
            ServiceLocator
                .Get<GameManager>()
                .Resume();
            
            ServiceLocator
                .Get<SceneLoader>()
                .LoadScene(Scene.MainMenu);
        }
        

        private void Open()
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