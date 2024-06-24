using System;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Text _gameOverText;
        [SerializeField] private Image lifeBar;
        [SerializeField] private GameObject _lifeBarGameObject;
        [SerializeField] private GameObject _upgradePicker;
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
            
            ActionManager.instance.OnCharacterLifeChange += OnCharacterLifeChange;
            ActionManager.instance.OnCharacterMaxLifeChange += OnCharacterMaxLifeChange;
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
            lifeBar.fillAmount = _currentLife / maxLife;

            if (lifeBar.fillAmount >= 0.5f)
            {
                lifeBar.color = LIFEBAR_GREEN; // Dark green
            }
            else if (lifeBar.fillAmount >= 0.25f)
            {
                lifeBar.color = LIFEBAR_YELLOW; // Dark yellow
            }
            else
            {
                lifeBar.color = LIFEBAR_RED; // Dark red
            }
        }

        private void OnCharacterMaxLifeChange(float oldMaxLife, float newMaxLife)
        {   
            float sizeMultiplier = newMaxLife / oldMaxLife;

            lifeBar.fillAmount = _currentLife / newMaxLife;

            Vector3 oldScale = _lifeBarGameObject.transform.localScale;
            _lifeBarGameObject.transform.localScale = new Vector3(oldScale.x * sizeMultiplier, oldScale.y, oldScale.z);
        }

        #endregion

        #region UPGRADES
        public void ShowUpgradePicker()
        {
            _upgradePicker.SetActive(true);
        }

        public void HideUpgradePicker()
        {
            _upgradePicker.SetActive(false);
        }

        #endregion

        public bool IsUpgradePickerActive()
        {
            return _upgradePicker.activeSelf;
        }
    }
}