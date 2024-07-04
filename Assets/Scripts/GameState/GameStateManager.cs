using Assets.Scripts.Upgrades;
using Menu;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class GameStateManager: MonoBehaviour
{

    private static readonly string GAMESTATE_FILE_NAME = "gameState.json";
    private static string GAMESTATE_FILE_PATH;

    #region SINGLETON
    public static GameStateManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            GAMESTATE_FILE_PATH = Path.Combine(Application.persistentDataPath, GAMESTATE_FILE_NAME);
            TryLoadGameState();
        }
        else
        {
            Destroy(this);
        }
    }
    #endregion


    public void SaveGameState()
    {
        float playerHealth = UIManager.instance.CurrentLife;
        List<UpgradeID> upgradeIDs = UpgradeManager.instance.GetAppliedUpgradeIDs();

        GameState gameState = new GameState(playerHealth, upgradeIDs);

        string json = JsonUtility.ToJson(gameState, true);

        File.WriteAllText(GAMESTATE_FILE_PATH, json);
        Debug.Log("Game state saved to " + GAMESTATE_FILE_PATH);
    }

    public void TryLoadGameState()
    {

        if (File.Exists(GAMESTATE_FILE_PATH))
        {
            string json = File.ReadAllText(GAMESTATE_FILE_PATH);
            if(json == null)
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
        // TODO: Restore game state
        //UIManager.instance.CurrentLife = gameState.playerHealth;
        UpgradeManager.instance.ApplyUpgrades(gameState.PlayerUpgrades);
        UIManager.instance.AddUpgradesToHolder(gameState.PlayerUpgrades);
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
}
