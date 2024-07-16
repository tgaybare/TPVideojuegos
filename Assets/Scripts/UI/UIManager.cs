using Assets.Scripts.UI;
using Assets.Scripts.Upgrades;
using Managers;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Text _gameOverText;
        [SerializeField] private Image _lifeBar;
        [SerializeField] private Text _lifeBarText;
        [SerializeField] private GameObject _lifeBarGameObject;
        [SerializeField] private GameObject _upgradePicker;
        private Card[] _cards;

        public float CurrentLife => _currentLife;
        private float _currentLife;

        private static readonly Color LIFEBAR_GREEN = new Color(0.21f, 0.46f, 0.04f);
        private static readonly Color LIFEBAR_YELLOW = new Color(0.85f, 0.79f, 0f);
        private static readonly Color LIFEBAR_RED = new Color(0.46f, 0.12f, 0.04f);

        #region SINGLETON
        public static UIManager instance;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;

                // Get all cards in the scene, should be 3
                _cards = _upgradePicker.transform.GetComponentsInChildren<Card>();
            }
            else
            {
                Destroy(this);
            }
        }
        #endregion

        private void Start()
        {
            ActionManager.instance.OnGameOver += OnGameOver;
            ActionManager.instance.OnCharacterMaxLifeChange += OnCharacterMaxLifeChange;
            ActionManager.instance.OnPlayerPickUpgrade += AddUpgradeToHolder;
            ActionManager.instance.OnPlayerEnterItemRoom += OnPlayerEnterItemRoom;
            ActionManager.instance.OnBossDefeated += ShowUpgradePicker;
            ActionManager.instance.OnCharacterLifeChange += OnCharacterLifeChange;

        }

        #region GAMEOVER

        private void OnGameOver(bool isVictory)
        {
            _gameOverText.text = isVictory ? "VICTORY" : "GAME OVER";
            _gameOverText.color = isVictory ? Color.green : Color.red;
            _gameOverText.gameObject.SetActive(true);
        }

        #endregion

        #region HUD_UI

        private void OnCharacterLifeChange(float currentLife, float maxLife)
        {
            _currentLife = currentLife;
            _lifeBar.fillAmount = _currentLife / maxLife;

            if (_lifeBar.fillAmount > 0.5f)
            {
                _lifeBar.color = LIFEBAR_GREEN; // Dark green
            }
            else if (_lifeBar.fillAmount >= 0.25f)
            {
                _lifeBar.color = LIFEBAR_YELLOW; // Dark yellow
            }
            else
            {
                _lifeBar.color = LIFEBAR_RED; // Dark red
            }

            _lifeBarText.text = $"{(int)(_currentLife / maxLife * 100)}%";
        }

        private void OnCharacterMaxLifeChange(float oldMaxLife, float newMaxLife)
        {
            float sizeMultiplier = newMaxLife / oldMaxLife;

            _lifeBar.fillAmount = _currentLife / newMaxLife; // I believe this is unnecessary

            Vector3 oldScale = _lifeBarGameObject.transform.localScale;
            _lifeBarGameObject.transform.localScale = new Vector3(oldScale.x * sizeMultiplier, oldScale.y, oldScale.z);
        }

        #endregion

        #region UPGRADES
        public void ShowUpgradePicker()
        {
            _upgradePicker.SetActive(true);
            GenerateUpgradeCards();
        }

        public void HideUpgradePicker()
        {
            _upgradePicker.SetActive(false);
        }

        public bool IsUpgradePickerActive()
        {
            return _upgradePicker.activeSelf;
        }

        private void GenerateUpgradeCards()
        {
            IAppliableUpgrade[] randomUpgrades = UpgradeManager.instance.GetRandomPickableUpgrades();

            for (int i = 0; i < _cards.Length; i++)
            {
                _cards[i].SetUpgradeInfo(randomUpgrades[i]);
            }
        }

        private void AddUpgradeToHolder(UpgradeID upgradeID)
        {
            HideUpgradePicker();

            IAppliableUpgrade upgrade = UpgradeManager.IDToUpgradeDict[upgradeID];
            UpgradeHolder.instance.AddUpgrade(upgrade);
        }

        public void AddUpgradesToHolder(List<UpgradeID> upgradeIDs)
        {
            foreach (UpgradeID upgradeID in upgradeIDs)
            {
                AddUpgradeToHolder(upgradeID);
            }
        }

        private void OnPlayerEnterItemRoom(bool alreadyVisied)
        {
            if (!alreadyVisied)
            {
                ShowUpgradePicker();
            }
        }

        #endregion


    }
}