using Assets.Scripts.Upgrades;
using Managers;
using Menu;
using Strategy.Strategy___Weapon;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static GameLevels;


public class GameStateManager : MonoBehaviour
{
    [SerializeField] private bool _isLoadScreen = false;
    private static readonly string GAMESTATE_FILE_NAME = "gameState.json";
    private static string GAMESTATE_FILE_PATH;


    private Levels _currentLevel = Levels.LEVEL_1;

    #region SINGLETON
    public static GameStateManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            OnAwake();
        }
        else
        {
            Destroy(this);
        }
    }
    #endregion

    private void OnAwake()
    {
        GAMESTATE_FILE_PATH = Path.Combine(Application.persistentDataPath, GAMESTATE_FILE_NAME);

        if (_isLoadScreen)
        {
            ClearGameState();
            return;
        }

        _currentLevel = CurrentLevel();
    }


    private void Start()
    {
        if (_isLoadScreen) return;

        if (_currentLevel != Levels.LEVEL_1)
        {
            TryLoadGameState();
        }

        ActionManager.instance.OnLevelComplete += OnLevelComplete;
    }

    public void SaveGameState(bool updateCurrentLevel = true)
    {
        //int playerHealth = (int) UIManager.instance.CurrentLife;
        LifeController playerLifeController = GameObject.FindGameObjectWithTag("Player").GetComponent<LifeController>();
        int playerHealth = (int)playerLifeController.Life;



        List<UpgradeID> upgradeIDs = UpgradeManager.instance.GetAppliedUpgradeIDs();

        Levels level = CurrentLevel();
        if (updateCurrentLevel && level < GameLevels.MAX_LEVEL)
        {
            level++;
        }

        GameState gameState = new GameState(playerHealth, upgradeIDs, level);

        string json = JsonUtility.ToJson(gameState, true);

        File.WriteAllText(GAMESTATE_FILE_PATH, json);
        Debug.Log("Game state saved to " + GAMESTATE_FILE_PATH);
    }

    public void TryLoadGameState()
    {

        if (File.Exists(GAMESTATE_FILE_PATH))
        {
            string json = File.ReadAllText(GAMESTATE_FILE_PATH);
            if (json == null)
            {
                Debug.LogWarning($"Could not load game state from '{GAMESTATE_FILE_PATH}' [JSON is null]");
                return;
            }

            GameState gameState = JsonUtility.FromJson<GameState>(json);
            if (gameState == null)
            {
                Debug.LogWarning($"Could not load game state from '{GAMESTATE_FILE_PATH}' [gameState is null]");
                return;
            }

            restoreGameState(gameState);
            Debug.Log("Game state loaded from " + GAMESTATE_FILE_PATH);
        }
        else
        {
            Debug.Log("No saved game state found at " + GAMESTATE_FILE_PATH);
        }
    }

    private void restoreGameState(GameState gameState)
    {
        // Restore Upgrades
        UpgradeManager.instance.ApplyUpgrades(gameState.PlayerUpgrades);
        UIManager.instance.AddUpgradesToHolder(gameState.PlayerUpgrades);

        // Restore Player Health
        LifeController playerLifeController = GameObject.FindGameObjectWithTag("Player").GetComponent<LifeController>();
        if (playerLifeController)
        {
            playerLifeController.UpdateLife(gameState.PlayerHealth);
        }
    }

    public void ClearGameState()
    {
        if (File.Exists(GAMESTATE_FILE_PATH))
        {
            File.Delete(GAMESTATE_FILE_PATH);
            Debug.Log("Game state file deleted from " + GAMESTATE_FILE_PATH);
        }
        else
        {
            Debug.LogWarning("No game state file found at " + GAMESTATE_FILE_PATH);
        }
    }

    public GameState GetGameState()
    {
        if (File.Exists(GAMESTATE_FILE_PATH))
        {
            string json = File.ReadAllText(GAMESTATE_FILE_PATH);
            return JsonUtility.FromJson<GameState>(json);
        }
        else
        {
            return null;
        }
    }

    public Levels CurrentLevel()
    {
        // If the current level is LEVEL_1, we need to check the saved game state
        // to seif the player has already completed a level, because it is the default value
        if (_currentLevel == Levels.LEVEL_1)
        {
            GameState gameState = GetGameState();

            if (gameState != null && gameState.CurrentLevel != Levels.LEVEL_1)
            {
                _currentLevel = gameState.CurrentLevel;
            }
        }
        return _currentLevel;
    }

    private void OnLevelComplete()
    {
        SaveGameState();
        UnitySceneManager.instance.LoadLevelAsync(_currentLevel + 1);
    }
}
