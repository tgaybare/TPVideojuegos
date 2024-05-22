using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
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

        public const string TITLE_SCREEN = "TitleScreen";
        public const string LOADING_SCREEN = "LoadingScreen";
        public const string GAME_SCREEN = "MainScene";
        public const string GAME_OVER_SCREEN = "GameOverScreen";
        public const string VICTORY_SCREEN = "VictoryScreen";
        
        public void LoadTitleScreen() => SceneManager.LoadScene(TITLE_SCREEN);
        
        public void LoadLoadingScreen() => SceneManager.LoadScene(LOADING_SCREEN);

        public AsyncOperation LoadGameScreenAsync() => SceneManager.LoadSceneAsync(GAME_SCREEN);

        public void LoadGameOverScreen() => SceneManager.LoadScene(GAME_OVER_SCREEN);

        public void LoadVictoryScreen() => SceneManager.LoadScene(VICTORY_SCREEN);

        
    }
}