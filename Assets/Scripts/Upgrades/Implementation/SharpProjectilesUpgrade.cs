using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Upgrades
{
    public class SharpProjectilesUpgrade : IAppliableUpgrade
    {

        private DistanceWeapon _distanceWeapon;
        private Sprite _projectileDamageUpgradeSprite;
        private const UpgradeID _upgradeID = UpgradeID.SHARP_PROJECTILES;
        private const float DAMAGE_MULTIPLIER = 1.5f;

        #region SINGLETON
        public static SharpProjectilesUpgrade Instance => GetInstance();
        private static SharpProjectilesUpgrade _instance;

        private SharpProjectilesUpgrade() { }

        private static SharpProjectilesUpgrade GetInstance()
        {
            if (_instance == null)
            {
                _instance = new SharpProjectilesUpgrade();
            }

            return _instance;
        }
        #endregion

        public void applyUpgrade()
        {
            if(_distanceWeapon == null)
            {
                Debug.LogError("Player DistanceWeapon not found");
                return;
            }

            IProjectile projectile = _distanceWeapon.ProjectilePrefab.GetComponent<IProjectile>();
            if(projectile == null)
            {
                Debug.LogError("Projectile does not implement IProjectile interface");
                return;
            }

            projectile.Damage = (int)(projectile.Damage * DAMAGE_MULTIPLIER);
        }

        public string GetDescription()
        {
            return $"Increases the damage of the projectile by {(DAMAGE_MULTIPLIER-1)*100}%";
        }

        public Sprite GetSprite()
        {
            return _projectileDamageUpgradeSprite;
        }

        public string GetTitle()
        {
            return "Sharp Projectiles";
        }

        public UpgradeID GetUpgradeID()
        {
            return _upgradeID;
        }

        public void Initialize()
        {
            _distanceWeapon = GameObject.FindGameObjectWithTag("Player").GetComponent<DistanceWeapon>();
            _projectileDamageUpgradeSprite = Resources.Load<Sprite>("Sprites/sharp-projectiles-upgrade");
        }
    }
}