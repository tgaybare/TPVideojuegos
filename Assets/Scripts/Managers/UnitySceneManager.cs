using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class UnitySceneManager : MonoBehaviour
    {
        
        public static UnitySceneManager instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        private const string MENU_SCREEN = "Menu";
        private const string FLOOR_1_SCREEN = "Floor_1";
        
        public void Load_MenuScreen() => SceneManager.LoadScene(MENU_SCREEN);
        public void Load_Floor1Screen() => SceneManager.LoadScene(FLOOR_1_SCREEN);
        
    }
}