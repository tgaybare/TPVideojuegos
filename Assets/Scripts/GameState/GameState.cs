using Assets.Scripts.Upgrades;
using Menu;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameState
{
    public Levels CurrentLevel;
    public float PlayerHealth;
    public List<UpgradeID> PlayerUpgrades;
    
    

    public GameState(float playerHealth, List<UpgradeID> playerUpgrades, Levels currentLevel = Levels.LEVEL_1)
    {
        Debug.Log($"Creating new game state with level {currentLevel}");
        CurrentLevel = currentLevel;
        PlayerHealth = playerHealth;
        PlayerUpgrades = playerUpgrades;
    }
}
