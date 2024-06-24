using Assets.Scripts.Upgrades;
using Managers;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class UpgradeManager : MonoBehaviour
{

    public static UpgradeManager instance;

    [SerializeField] private Dictionary<UpgradeID, IAppliableUpgrade> _appliedUpgrades = new();
    [SerializeField] private Dictionary<UpgradeID, IAppliableUpgrade> _availableUpgrades = new() {
        { UpgradeID.MORE_HP, HealthUpgrade.Instance },
        { UpgradeID.MORE_SPEED, SpeedUpgrade.Instance },
        { UpgradeID.DOUBLE_SHOT, DoubleShotUpgrade.Instance }
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
}
