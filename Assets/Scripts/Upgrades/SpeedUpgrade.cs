using Strategy.Strategy___Weapon;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Upgrades
{
    public class SpeedUpgrade : IAppliableUpgrade
    {
        private MovementController _playerMovementController;
        private const UpgradeID _upgradeID = Upgrades.UpgradeID.MORE_SPEED;
        private const float EXTRA_SPEED_MULTIPLIER = 1.3f; // 30% more speed

        #region SINGLETON   
        public static SpeedUpgrade Instance => GetInstance();
        private static SpeedUpgrade _instance;

        private SpeedUpgrade() { }

        private static SpeedUpgrade GetInstance()
        {
            if (_instance == null)
            {
                _instance = new SpeedUpgrade();
            }

            return _instance;
        }
        #endregion

        public void applyUpgrade()
        {
            _playerMovementController.SpeedMultiplier = EXTRA_SPEED_MULTIPLIER;
        }

        public string Description()
        {
            return "You move faster now!";
        }

        public void Initialize()
        {
            _playerMovementController = GameObject.FindGameObjectWithTag("Player").GetComponent<MovementController>();
        }

        public string Title()
        {
            return "Speed Upgrade";
        }

        public UpgradeID UpgradeID()
        {
            return _upgradeID;
        }

       
    }
}