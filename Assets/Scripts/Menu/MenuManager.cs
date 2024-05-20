using UnityEngine;

namespace Managers
{
    public class MenuManager : MonoBehaviour
    {
        public void PlayButton()
        {
            UnitySceneManager.instance.LoadGameScreen();
        }

        public void ExitButton()
        {
            Application.Quit();
        }
    }
}