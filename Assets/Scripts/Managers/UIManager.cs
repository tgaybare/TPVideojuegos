﻿using System;
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

        private static readonly Color LIFEBAR_GREEN = new Color(0.21f, 0.46f, 0.04f);
        private static readonly Color LIFEBAR_YELLOW = new Color(0.85f, 0.79f, 0f);
        private static readonly Color LIFEBAR_RED = new Color(0.46f, 0.12f, 0.04f);
        
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

        #endregion
        
    }
}