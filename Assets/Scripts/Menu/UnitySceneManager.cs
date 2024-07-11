using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GameLevels;


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

    public static string TITLE_SCREEN = "TitleScreen";
    public static string LOADING_SCREEN = "LoadingScreen";
    public static string GAME_OVER_SCREEN = "GameOverScreen";
    public static string VICTORY_SCREEN = "VictoryScreen";

    private static Dictionary<Levels, string> _levelSceneNames = new Dictionary<Levels, string>
    {
        {Levels.LEVEL_1, "Level1"},
        {Levels.LEVEL_2, "Level2"}
    };
        
    public void LoadTitleScreen() => SceneManager.LoadScene(TITLE_SCREEN);
        
    public void LoadLoadingScreen() => SceneManager.LoadScene(LOADING_SCREEN);

    public AsyncOperation LoadLevelAsync(Levels level) 
    {
        if (!_levelSceneNames.ContainsKey(level))
            throw new ArgumentException("Level not found in dictionary");

        return SceneManager.LoadSceneAsync(_levelSceneNames[level]); 
    }

    public void LoadGameOverScreen() => SceneManager.LoadScene(GAME_OVER_SCREEN);

    public void LoadVictoryScreen() => SceneManager.LoadScene(VICTORY_SCREEN);
        
}

