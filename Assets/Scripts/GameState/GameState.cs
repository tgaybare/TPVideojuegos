using Assets.Scripts.Upgrades;
using System;
using System.Collections.Generic;
using static GameLevels;

[Serializable]
public class GameState
{
    public Levels CurrentLevel;
    public int PlayerHealth;
    public List<UpgradeID> PlayerUpgrades;



    public GameState(int playerHealth, List<UpgradeID> playerUpgrades, Levels currentLevel = Levels.LEVEL_1)
    {
        CurrentLevel = currentLevel;
        PlayerHealth = playerHealth;
        PlayerUpgrades = playerUpgrades;
    }
}
