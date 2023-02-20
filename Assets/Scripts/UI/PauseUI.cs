using EmreBeratKR.ServiceLocator;
using UnityEngine;

namespace UI
{
    public class PauseUI : MonoBehaviour
    {
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
            Show();
        }
        
        private void OnGameUnPaused()
        {
            Hide();
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

        public void OnClickedMainMenu()
        {
            ServiceLocator
                .Get<GameManager>()
                .Resume();
            
            ServiceLocator
                .Get<SceneLoader>()
                .LoadScene(Scene.MainMenu);
        }
        

        private void Show()
        {
            main.SetActive(true);
        }

        private void Hide()
        {
            main.SetActive(false);
        }
    }
}