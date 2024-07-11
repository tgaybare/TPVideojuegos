using Assets.Scripts.Upgrades;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UpgradeHolder : MonoBehaviour
{
    private const int MAX_DISPLAYED_UPGRADES = 7;

    [SerializeField] private List<UpgradeIcon> _upgradeIcons = new();
    FixedSizedQueue<IAppliableUpgrade> _shownUpgrades = new(MAX_DISPLAYED_UPGRADES);

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
        /*foreach (UpgradeIcon icon in _upgradeIcons)
        {
            icon.gameObject.SetActive(false);
        }*/
    }

    public void AddUpgrade(IAppliableUpgrade upgrade)
    {
        _shownUpgrades.Enqueue(upgrade);

        if(_shownUpgrades.Count == MAX_DISPLAYED_UPGRADES)
        {
            if (!_upgradeIcons[0].gameObject.activeSelf) 
            { 
                _upgradeIcons[0].gameObject.SetActive(true);                       
            }

            shiftIcons();
        } else 
        {
            UpgradeIcon nextUpgradeIcon = _upgradeIcons[MAX_DISPLAYED_UPGRADES - _shownUpgrades.Count];
            nextUpgradeIcon.gameObject.SetActive(true);
            nextUpgradeIcon.UpdateUpgrade(upgrade.GetUpgradeID(), upgrade.GetSprite());
        }
    }

    // Update the icons to show the new upgrades, shifting the icons to the right
    // and removing the oldest one
    private void shiftIcons() {
        IAppliableUpgrade[] toShow = _shownUpgrades.ToArray();
        
        for (int i = 0; i < MAX_DISPLAYED_UPGRADES; i++)
        {
            _upgradeIcons[MAX_DISPLAYED_UPGRADES - i - 1].UpdateUpgrade(toShow[i].GetUpgradeID(), toShow[i].GetSprite());
        }
    }
        
}
