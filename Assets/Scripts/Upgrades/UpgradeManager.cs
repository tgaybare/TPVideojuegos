﻿using Assets.Scripts.Upgrades;
using Managers;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class UpgradeManager : MonoBehaviour
{

    

    [SerializeField] private Dictionary<UpgradeID, IAppliableUpgrade> _appliedUpgrades = new();
    [SerializeField] private Dictionary<UpgradeID, IAppliableUpgrade> _availableUpgrades = new() {
        { UpgradeID.MORE_HP, HealthUpgrade.Instance },
        { UpgradeID.MORE_SPEED, SpeedUpgrade.Instance },
        { UpgradeID.DOUBLE_SHOT, DoubleShotUpgrade.Instance }
    };

    #region SINGLETON
    public static UpgradeManager instance;

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
    #endregion

    public void Start()
    {
        // Initialize all upgrades
        foreach (IAppliableUpgrade upgrade in _availableUpgrades.Values)
        {
            upgrade.Initialize();
        }
    }

    public void ApplyUpgrade(UpgradeID upgradeID)
    {
        if (_appliedUpgrades.ContainsKey(upgradeID))
        {
            Debug.LogError($"IAppliableUpgrade with ID = {upgradeID} already applied");
            return;
        }

        if(!_availableUpgrades.ContainsKey(upgradeID))
        {
            Debug.LogError($"IAppliableUpgrade with ID = {upgradeID} not found");
            return;
        }

        IAppliableUpgrade toApply = _availableUpgrades[upgradeID];
        toApply.applyUpgrade();

        _appliedUpgrades.Add(upgradeID, toApply);
        _availableUpgrades.Remove(upgradeID);
    }

    // Returns a random array of 3 upgrades that can be picked
    public IAppliableUpgrade[] GetRandomPickableUpgrades()
    {
        IAppliableUpgrade[] result = new IAppliableUpgrade[3];

        List<IAppliableUpgrade> upgrades = new List<IAppliableUpgrade>(_availableUpgrades.Values);
        for (int i = 0; i < 3; i++)
        {
            int randomIndex = Random.Range(0, upgrades.Count);
            result[i] = upgrades[randomIndex];
            upgrades.RemoveAt(randomIndex);

            Debug.Log($"Upgrade {i}: {result[i].GetTitle()}");
        }
        return result;
    }
}
