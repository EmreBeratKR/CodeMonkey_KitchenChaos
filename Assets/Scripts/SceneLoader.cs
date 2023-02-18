using System;
using System.Collections;
using EmreBeratKR.ServiceLocator;
using General;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : ServiceBehaviour, IProgressProvider
{
    [SerializeField] private GameObject main;
    
    
    public event Action<ProgressChangedArgs> OnProgressChanged;
    
    
    public void LoadScene(Scene scene)
    {
        var operation = SceneManager.LoadSceneAsync((int) scene);
        StartCoroutine(Routine());
        
        IEnumerator Routine()
        {
            main.gameObject.SetActive(true);
            
            while (!operation.isDone)
            {
                OnProgressChanged?.Invoke(new ProgressChangedArgs
                {
                    progressNormalized = operation.progress
                });

                yield return null;
            }
            
            main.gameObject.SetActive(false);
        }
    }
}

public enum Scene
{
    MainMenu,
    Game
}