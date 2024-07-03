using Assets.Scripts.Upgrades;
using Managers;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class UpgradeManager : MonoBehaviour
{
    public static Dictionary<UpgradeID, IAppliableUpgrade> IDToUpgradeDict { get; } = new Dictionary<UpgradeID, IAppliableUpgrade>()
    {
        { UpgradeID.MORE_HP, HealthUpgrade.Instance },
        { UpgradeID.MORE_SPEED, SpeedUpgrade.Instance },
        { UpgradeID.DOUBLE_SHOT, DoubleShotUpgrade.Instance },
        { UpgradeID.SHARP_PROJECTILES, SharpProjectilesUpgrade.Instance },
        { UpgradeID.EXPLOSIVE_SHOT, ExplosiveShotUpgrade.Instance}
    };

    private Dictionary<UpgradeID, IAppliableUpgrade> _appliedUpgrades = new();
    private Dictionary<UpgradeID, IAppliableUpgrade> _availableUpgrades = new();

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

        ActionManager.instance.OnPlayerPickUpgrade += ApplyUpgrade;

        // Initialize all upgrades
        foreach (IAppliableUpgrade upgrade in IDToUpgradeDict.Values)
        {
            upgrade.Initialize();
        }

        _availableUpgrades = new Dictionary<UpgradeID, IAppliableUpgrade>(IDToUpgradeDict);
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

        int cardsToPick = upgrades.Count >= 3 ? 3 : upgrades.Count;
        for (int i = 0; i < cardsToPick; i++)
        {
            int randomIndex = Random.Range(0, upgrades.Count);
            result[i] = upgrades[randomIndex];
            upgrades.RemoveAt(randomIndex);
        }
        return result;
    }
}
