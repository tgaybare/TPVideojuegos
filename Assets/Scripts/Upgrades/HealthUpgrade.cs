﻿using Strategy.Strategy___Weapon;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Upgrades
{
    public sealed class HealthUpgrade : IAppliableUpgrade
    {

        private const UpgradeID _upgradeID = Upgrades.UpgradeID.MORE_HP;
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
            return "HP IAppliableUpgrade";
        }

        public string Description()
        {
            return $"Increments your Max Health by {(EXTRA_HEALTH_MULTIPLIER - 1) * 100}%";
        }
    }
}