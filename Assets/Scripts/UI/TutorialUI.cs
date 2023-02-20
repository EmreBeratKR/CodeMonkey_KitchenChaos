using UnityEngine;

namespace UI
{
    public class TutorialUI : MonoBehaviour
    {
        [SerializeField] private GameObject main;
        
        
        private void OnEnable()
        {
            GameManager.OnBeginTutorial += OnBeginTutorial;
            GameManager.OnCompleteTutorial += OnCompleteTutorial;
        }

        private void OnDisable()
        {
            GameManager.OnBeginTutorial -= OnBeginTutorial;
            GameManager.OnCompleteTutorial -= OnCompleteTutorial;
        }

        private void OnBeginTutorial()
        {
            main.SetActive(true);
        }
        
        private void OnCompleteTutorial()
        {
            main.SetActive(false);
        }
    }
}