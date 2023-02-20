using System;
using System.Collections;
using EmreBeratKR.ServiceLocator;
using General;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : ServiceBehaviour, IProgressProvider
{
    [SerializeField] private GameObject main;
    
    
    public static event Action<SceneLoadedArgs> OnSceneLoaded; 
    public struct SceneLoadedArgs
    {
        public Scene scene;
    }
    
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
                Debug.Log(operation.progress);
                
                OnProgressChanged?.Invoke(new ProgressChangedArgs
                {
                    progressNormalized = operation.progress
                });

                yield return null;
            }
            
            main.gameObject.SetActive(false);
            
            OnSceneLoaded?.Invoke(new SceneLoadedArgs
            {
                scene = scene
            });
        }
    }
}

public enum Scene
{
    MainMenu,
    Game
}