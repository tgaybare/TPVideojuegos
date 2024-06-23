using System;
using Menu;
using UnityEngine;

namespace Managers
{
    public class ActionManager : MonoBehaviour
    {

        public static ActionManager instance;
        
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        #region GAME_MANAGER_ACTIONS
        
        public event Action<bool> OnGameOver;

        public void ActionGameOver(bool isVictory)
        {
            if (OnGameOver != null)
            {
                OnGameOver(isVictory);

                if (isVictory)
                {
                    Invoke(nameof(LoadVictoryScreen),5f);
                }
                else
                {
                    Invoke(nameof(LoadGameOverScreen),5f);
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

        #endregion GAME_MANAGER_ACTIONS
        
        #region HUD_UI_ACTIONS

        public event Action<float,float> OnCharacterLifeChange;
        // public event Action<int, int> OnWeaponChange;
        
        public void CharacterLifeChange(float currentLife, float maxLife)
        {
            if (OnCharacterLifeChange != null)
            {
                OnCharacterLifeChange(currentLife, maxLife);
            }
        }

        public event Action<float,float> OnCharacterMaxLifeChange;

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

        

        #endregion

    }
}