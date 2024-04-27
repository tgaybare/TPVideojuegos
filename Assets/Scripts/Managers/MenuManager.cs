using UnityEngine;

namespace Managers
{
    public class MenuManager : MonoBehaviour
    {
        public void PlayButton()
        {
            UnitySceneManager.instance.Load_Floor1Screen();
        }
    }
}