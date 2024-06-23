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
        private float _currentLife;
        
        private void Start()
        {
            ActionManager.instance.OnGameOver += OnGameOver;
            
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
            lifeBar.fillAmount = _currentLife / maxLife;
        }

        #endregion
        
    }
}