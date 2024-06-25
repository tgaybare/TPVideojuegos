using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Upgrades
{
    public class ExplosiveShotUpgrade : IAppliableUpgrade
    {
        private Sprite _explosiveShotUpgradeSprite;
        private const UpgradeID _upgradeID = UpgradeID.EXPLOSIVE_SHOT;
        private DistanceWeapon _distanceWeapon;

        #region SINGLETON
        public static ExplosiveShotUpgrade Instance => GetInstance();
        private static ExplosiveShotUpgrade _instance;

        private ExplosiveShotUpgrade() { }

        private static ExplosiveShotUpgrade GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ExplosiveShotUpgrade();
            }

            return _instance;
        }
        #endregion

        public void Initialize()
        {
            _distanceWeapon = GameObject.FindGameObjectWithTag("Player").GetComponent<DistanceWeapon>();
            _explosiveShotUpgradeSprite = Resources.Load<Sprite>("Sprites/explosive-shot-upgrade");
        }

        public void applyUpgrade()
        {
            if (_distanceWeapon == null)
            {
                Debug.LogError("Player DistanceWeapon not found");
                return;
            }

            _distanceWeapon.ExplosiveShot = true;
        }

        public string GetDescription()
        {
            return "Your projectiles now explode on impact!";
        }

        public string GetTitle()
        {
            return "Explosive Shot";
        }

        public UpgradeID GetUpgradeID()
        {
            return _upgradeID;
        }

        public Sprite GetSprite()
        {
            return _explosiveShotUpgradeSprite;
        }
    }
}