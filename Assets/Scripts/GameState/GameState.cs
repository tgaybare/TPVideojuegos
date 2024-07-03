using Assets.Scripts.Upgrades;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameState
{
    public float PlayerHealth;
    public List<UpgradeID> PlayerUpgrades;
    
    public GameState(float playerHealth, List<UpgradeID> playerUpgrades)
    {
        PlayerHealth = playerHealth;
        PlayerUpgrades = playerUpgrades;
    }
}
