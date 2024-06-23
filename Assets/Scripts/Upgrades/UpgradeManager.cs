using Assets.Scripts.Upgrades;
using Managers;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class UpgradeManager : MonoBehaviour
{

    public static UpgradeManager instance;

    [SerializeField] private Dictionary<UpgradeID, Upgrade> _appliedUpgrades = new();
    [SerializeField] private Dictionary<UpgradeID, Upgrade> _availableUpgrades = new() {
        { UpgradeID.MORE_HP, MoreHPUpgrade.Instance }
    };

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else
        {
            Destroy(this);
        }
    }

    public void Start()
    {
        // Initialize all upgrades
        foreach (Upgrade upgrade in _availableUpgrades.Values)
        {
            upgrade.Initialize();
        }
    }

    public void ApplyUpgrade(UpgradeID upgradeID)
    {
        if (_appliedUpgrades.ContainsKey(upgradeID))
        {
            Debug.LogError($"Upgrade with ID = {upgradeID} already applied");
            return;
        }

        if(!_availableUpgrades.ContainsKey(upgradeID))
        {
            Debug.LogError($"Upgrade with ID = {upgradeID} not found");
            return;
        }

        Upgrade toApply = _availableUpgrades[upgradeID];
        toApply.applyUpgrade();

        _appliedUpgrades.Add(upgradeID, toApply);
        _availableUpgrades.Remove(upgradeID);
    }
}
