using CounterSystem;
using EmreBeratKR.ServiceLocator;
using TMPro;
using UnityEngine;

namespace UI
{
    public class GameOverUI : MonoBehaviour
    {
        [SerializeField] private GameObject main;
        [SerializeField] private TMP_Text deliveredRecipeCountField;
        
        
        private void OnEnable()
        {
            GameManager.OnGameOver += OnGameOver;
            
            ServiceLocator
                .Get<DeliveryCounter>()
                .OnDeliverySucceed += OnDeliverySucceed;
        }

        private void OnDisable()
        {
            GameManager.OnGameOver -= OnGameOver;
            
            ServiceLocator
                .Get<DeliveryCounter>()
                .OnDeliverySucceed -= OnDeliverySucceed;
        }


        public void OnClickedRestart()
        {
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
        

        private void OnGameOver()
        {
            main.SetActive(true);
        }

        private void OnDeliverySucceed(DeliveryCounter.DeliverySucceedArgs args)
        {
            deliveredRecipeCountField.text = args.deliveredRecipeCount.ToString();
        }
    }
}