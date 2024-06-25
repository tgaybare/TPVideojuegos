using System;
using System.Collections;
using System.Xml.Linq;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Upgrades
{
    public interface IAppliableUpgrade
    {
        // This method is used to initialize the upgrade, for example, to get a reference to the player's life controller
        // This is useful because we don't want Upgrade to inherit from MonoBehaviour
        public void Initialize();
        public void applyUpgrade();

        public abstract UpgradeID GetUpgradeID();

        public string GetTitle();

        public string GetDescription();

    }
}