using Assets.Scripts.Upgrades;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class UpgradeIcon : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private UpgradeID _upgradeID = UpgradeID.NONE;

    private void Awake()
    {
        _image = GetComponentInChildren<Image>();
    }

    public void UpdateUpgrade(UpgradeID upgradeID, Sprite upgradeSprite)
    {
        _image.sprite = upgradeSprite;
        _upgradeID = upgradeID;
    }
}
