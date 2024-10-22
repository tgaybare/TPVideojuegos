﻿using Assets.Scripts.Upgrades;
using System;
using UnityEngine;

namespace Managers
{
    public class ActionManager : MonoBehaviour
    {

        #region SINGLETON
        public static ActionManager instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }
        #endregion

        #region GAME_MANAGER_ACTIONS

        public event Action<bool> OnGameOver;

        public void ActionGameOver(bool isVictory)
        {
            if (OnGameOver != null)
            {
                OnGameOver(isVictory);

                if (isVictory)
                {
                    // TODO: Do not hardcode
                    Invoke(nameof(LoadVictoryScreen), 5f);
                }
                else
                {
                    Invoke(nameof(LoadGameOverScreen), 5f);
                }
            }
        }

        public event Action<bool> OnGameStart;
        public void ActionGameStart()
        {
            throw new NotImplementedException();
        }

        private void LoadTitleScreen() => UnitySceneManager.instance.LoadTitleScreen();

        private void LoadGameOverScreen() => UnitySceneManager.instance.LoadGameOverScreen();

        private void LoadVictoryScreen() => UnitySceneManager.instance.LoadVictoryScreen();

        public event Action OnBossDefeated;

        public void ActionBossDefeated()
        {
            if (OnBossDefeated != null)
            {
                OnBossDefeated();
            }
        }

        public event Action OnLevelComplete;

        public void ActionLevelComplete()
        {
            if (OnLevelComplete != null)
            {
                OnLevelComplete();
            }
        }

        #endregion GAME_MANAGER_ACTIONS

        #region HUD_UI_ACTIONS

        public event Action<float, float> OnCharacterLifeChange;
        // public event Action<int, int> OnWeaponChange;

        public void CharacterLifeChange(float currentLife, float maxLife)
        {
            if (OnCharacterLifeChange != null)
            {
                OnCharacterLifeChange(currentLife, maxLife);
            }
        }

        public event Action<float, float> OnCharacterMaxLifeChange;

        public void CharacterMaxLifeChange(float oldMaxLife, float newMaxLife)
        {
            if (OnCharacterMaxLifeChange != null)
            {
                OnCharacterMaxLifeChange(oldMaxLife, newMaxLife);
            }
        }


        public void WeaponChange(int currentAmmo, int maxAmmo)
        {
            // if (OnWeaponChange != null)
            // {
            //     OnWeaponChange(currentAmmo, maxAmmo);
            // }
            throw new NotImplementedException();
        }

        #endregion

        #region GAME_ACTIONS

        public event Action<GameObject> OnEnemyKilled;
        public void ActionEnemyKilled(GameObject enemy)
        {
            if (OnEnemyKilled != null)
            {
                OnEnemyKilled(enemy);
            }
        }

        public event Action<Room> OnPlayerEnterRoom;
        public void ActionPlayerEnterRoom(Room newRoom)
        {
            if (OnPlayerEnterRoom != null)
            {
                OnPlayerEnterRoom(newRoom);
            }
        }

        public event Action<Room> OnPlayerExitRoom;
        public void ActionPlayerExitRoom(Room oldRoom)
        {
            if (OnPlayerExitRoom != null)
            {
                OnPlayerExitRoom(oldRoom);
            }
        }

        public event Action<bool> OnPlayerEnterItemRoom;

        public void ActionPlayerEnterItemRoom(bool alreadyVisited)
        {
            if (OnPlayerEnterItemRoom != null)
            {
                OnPlayerEnterItemRoom(alreadyVisited);
            }
        }

        public event Action<UpgradeID> OnPlayerPickUpgrade;

        public void ActionPlayerPickUpgrade(UpgradeID upgradeID)
        {
            if (OnPlayerPickUpgrade != null)
            {
                OnPlayerPickUpgrade(upgradeID);
            }
        }
        #endregion


    }
}