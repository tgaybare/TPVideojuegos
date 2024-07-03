using Assets.Scripts.Upgrades;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UpgradeHolder : MonoBehaviour
{
    [SerializeField] private List<UpgradeIcon> _upgradeIcons = new();
    FixedSizedQueue<IAppliableUpgrade> _shownUpgrades;

    private int _maxUpgradeCount => _upgradeIcons.Count;

    #region SINGLETON
    public static UpgradeHolder instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    #endregion SINGLETON

    void Start()
    {
        _shownUpgrades = new(_maxUpgradeCount);
    }

    public void AddUpgrade(IAppliableUpgrade upgrade)
    {
        _shownUpgrades.Enqueue(upgrade);

        if(_shownUpgrades.Count == _maxUpgradeCount)
        {
            shiftIcons();
        } else 
        {
            _upgradeIcons[_maxUpgradeCount - _shownUpgrades.Count].UpdateUpgrade(upgrade.GetUpgradeID(), upgrade.GetSprite());
        }
    }

    // Update the icons to show the new upgrades, shifting the icons to the right
    // and removing the oldest one
    private void shiftIcons() {
        IAppliableUpgrade[] toShow = _shownUpgrades.ToArray();
        
        for (int i = 0; i < _maxUpgradeCount; i++)
        {
            _upgradeIcons[i].UpdateUpgrade(toShow[i].GetUpgradeID(), toShow[i].GetSprite());
        }
    }
        
}
