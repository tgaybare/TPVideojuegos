using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Upgrades
{
    public class DoubleShotUpgrade : IAppliableUpgrade
    {
        private const UpgradeID _upgradeID = Upgrades.UpgradeID.DOUBLE_SHOT;
        private DistanceWeapon _distanceWeapon;

        #region SINGLETON
        public static DoubleShotUpgrade Instance => GetInstance();
        private static DoubleShotUpgrade _instance;

        private DoubleShotUpgrade() { }

        private static DoubleShotUpgrade GetInstance()
        {
            if (_instance == null)
            {
                _instance = new DoubleShotUpgrade();
            }

            return _instance;
        }
        #endregion

        public void applyUpgrade()
        {
            if (_distanceWeapon == null)
            {
                Debug.LogError("Player DistanceWeapon not found");
                return;
            }

            _distanceWeapon.ProjectilesPerAttack = 2;
        }

        public string GetDescription()
        {
            return "You can now shoot two projectiles at once!";
        }

        public void Initialize()
        {
            _distanceWeapon = GameObject.FindGameObjectWithTag("Player").GetComponent<DistanceWeapon>();
        }

        public string GetTitle()
        {
            return "Double Shot";
        }

        public UpgradeID GetUpgradeID()
        {
            return _upgradeID;
        }
    }
}