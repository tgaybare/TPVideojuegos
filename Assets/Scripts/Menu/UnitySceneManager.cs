using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class UnitySceneManager : MonoBehaviour
    {
        #region SINGLETON
        public static UnitySceneManager instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }
        #endregion SINGLETON

        private const string TITLE_SCREEN = "TitleScreen";
        private const string GAME_SCREEN = "MainScene";
        
        public void LoadTitleScreen() => SceneManager.LoadScene(TITLE_SCREEN);
        public void LoadGameScreen() => SceneManager.LoadScene(GAME_SCREEN);
        
    }
}