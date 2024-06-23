using Strategy.Strategy___Weapon;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Upgrades
{
    public sealed class MoreHPUpgrade : Upgrade
    {

        public static MoreHPUpgrade Instance => GetInstance();
        private static MoreHPUpgrade _instance;
        

        private const UpgradeID _upgradeID = Upgrades.UpgradeID.MORE_HP;
        private float EXTRA_HEALTH_MULTIPLIER = 1.5f;

        private LifeController _playerLifeController;

        private MoreHPUpgrade() { }

        private static MoreHPUpgrade GetInstance() {
            if (_instance == null)
            {
                _instance = new MoreHPUpgrade();
            }

            return _instance;
        }

        public void Initialize()
        {
            _playerLifeController = _playerLifeController = GameObject.FindGameObjectWithTag("Player").GetComponent<LifeController>();
        }

        public void applyUpgrade()
        {
            if (_playerLifeController == null){
                Debug.LogError("Player Life Controller not found");
                return;
            }

            _playerLifeController.incrementMaxLife(EXTRA_HEALTH_MULTIPLIER);
        }

        public UpgradeID UpgradeID()
        {
            return _upgradeID;
        }

        public string Title()
        {
            return "HP Upgrade";
        }

        public string Description()
        {
            return $"Increments your Max Health by {(EXTRA_HEALTH_MULTIPLIER - 1) * 100}%";
        }
    }
}