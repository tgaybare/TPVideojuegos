using UnityEngine;

namespace Managers
{
    public class MenuManager : MonoBehaviour
    {
        public void PlayButton()
        {
            UnitySceneManager.instance.LoadLoadingScreen();
        }

        public void ExitButton()
        {
            Application.Quit();
        }
    }
}