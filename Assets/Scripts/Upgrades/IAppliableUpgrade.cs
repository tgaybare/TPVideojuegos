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
        /*public bool IsApplied { get => _isApplied; }
        private bool _isApplied = false;*/

        public void applyUpgrade();

        public abstract UpgradeID UpgradeID();

        // This method is used to initialize the upgrade, for example, to get a reference to the player's life controller
        // This is useful because we don't want Upgrade to inherit from MonoBehaviour
        public void Initialize();

        public string Title();

        public string Description();

    }
}