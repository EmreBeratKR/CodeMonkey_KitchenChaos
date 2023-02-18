using EmreBeratKR.ServiceLocator;
using UnityEngine;

namespace UI
{
    public class MainMenuUI : MonoBehaviour
    {
        public void OnClickedPlay()
        {
            ServiceLocator.Get<SceneLoader>().LoadScene(Scene.Game);
        }

        public void OnClickedQuit()
        {
            Application.Quit();
        }
    }
}
