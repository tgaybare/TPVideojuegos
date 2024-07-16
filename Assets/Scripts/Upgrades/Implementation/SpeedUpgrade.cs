using UnityEngine;

namespace Assets.Scripts.Upgrades
{
    public class SpeedUpgrade : IAppliableUpgrade
    {
        private Sprite _speedUpgradeSprite;
        private MovementController _playerMovementController;
        private const UpgradeID _upgradeID = UpgradeID.MORE_SPEED;
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

        public void Initialize()
        {
            _playerMovementController = GameObject.FindGameObjectWithTag("Player").GetComponent<MovementController>();
            _speedUpgradeSprite = Resources.Load<Sprite>("Sprites/speed-upgrade");
        }

        public void applyUpgrade()
        {
            if (_playerMovementController == null)
            {
                Debug.LogError("Player MovementController not found");
                return;
            }

            _playerMovementController.SpeedMultiplier = EXTRA_SPEED_MULTIPLIER;
        }

        public string GetDescription()
        {
            return "You move faster now!";
        }

        public string GetTitle()
        {
            return "Speed Upgrade";
        }

        public UpgradeID GetUpgradeID()
        {
            return _upgradeID;
        }

        public Sprite GetSprite()
        {
            return _speedUpgradeSprite;
        }


    }
}