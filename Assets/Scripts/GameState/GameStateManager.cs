using Assets.Scripts.Upgrades;
using Menu;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class GameStateManager
{

    private static readonly string GAMESTATE_FILE_NAME = "gameState.json";
    private static readonly string GAMESTATE_FILE_PATH = Path.Combine(Application.persistentDataPath, GAMESTATE_FILE_NAME);

    #region SINGLETON
    public static GameStateManager Instance => GetInstance();
    private static GameStateManager _instance;

    private GameStateManager() { }

    private static GameStateManager GetInstance()
    {
        if (_instance == null)
        {
            _instance = new GameStateManager();
        }

        return _instance;
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

    public void LoadGameState()
    {

        if (File.Exists(GAMESTATE_FILE_PATH))
        {
            string json = File.ReadAllText(GAMESTATE_FILE_PATH);
            GameState gameState = JsonUtility.FromJson<GameState>(json);

            // TODO: Restore game state
            //UIManager.instance.CurrentLife = gameState.playerHealth;
            UpgradeManager.instance.ApplyUpgrades(gameState.PlayerUpgrades);
            UIManager.instance.AddUpgradesToHolder(gameState.PlayerUpgrades);

            Debug.Log("Game state loaded from " + GAMESTATE_FILE_PATH);
        }
        else
        {
            Debug.Log("No saved game state found at " + GAMESTATE_FILE_PATH);
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
}
