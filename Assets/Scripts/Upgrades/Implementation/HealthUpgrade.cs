using Strategy.Strategy___Weapon;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Upgrades
{
    public sealed class HealthUpgrade : IAppliableUpgrade
    {
        private Sprite _healthUpgradeSprite;
        private const UpgradeID _upgradeID = UpgradeID.MORE_HP;
        private float EXTRA_HEALTH_MULTIPLIER = 1.5f;

        private LifeController _playerLifeController;

        #region SINGLETON
        public static HealthUpgrade Instance => GetInstance();
        private static HealthUpgrade _instance;

        private HealthUpgrade() { }

        private static HealthUpgrade GetInstance() {
            if (_instance == null)
            {
                _instance = new HealthUpgrade();
            }

            return _instance;
        }
        #endregion

        public void Initialize()
        {
            _playerLifeController = _playerLifeController = GameObject.FindGameObjectWithTag("Player").GetComponent<LifeController>();
            _healthUpgradeSprite = Resources.Load<Sprite>("Sprites/health-upgrade");
        }

        public void applyUpgrade()
        {
            if (_playerLifeController == null){
                Debug.LogError("Player LifeController not found");
                return;
            }

            _playerLifeController.incrementMaxLife(EXTRA_HEALTH_MULTIPLIER);
        }

        public UpgradeID GetUpgradeID()
        {
            return _upgradeID;
        }

        public string GetTitle()
        {
            return "More Health";
        }

        public string GetDescription()
        {
            return $"Increments your Max Health by {(EXTRA_HEALTH_MULTIPLIER - 1) * 100}%";
        }

        public Sprite GetSprite()
        {
            return _healthUpgradeSprite;
        }
    }
}