using System;
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
            Debug.Log("created instance of ActionManager");
        }

        #region GAME_MANAGER_ACTIONS
        
        public event Action<bool> OnGameOver;

        public void ActionGameOver(bool isVictory)
        {
            if (OnGameOver != null)
            {
                OnGameOver(isVictory);
                Invoke(nameof(LoadTitleScreen),5f);
            }
        }
        
        public event Action<bool> OnGameStart;
        public void ActionGameStart()
        {
          throw new NotImplementedException();
        }

        private void LoadTitleScreen() => UnitySceneManager.instance.LoadTitleScreen();

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

        // Al final no son actions
        
        // public event Action OnBoltHit;
        // public event Action OnCrossbowShot;
        // public event Action OnSwordSlash;
        //
        // public void BoltHit()
        // {
        //     if (OnBoltHit != null)
        //     {
        //         OnBoltHit();
        //     }
        // }
        //
        // public void CrossbowShot()
        // {
        //     if (OnCrossbowShot != null)
        //     {
        //         OnCrossbowShot();
        //     }
        // }
        //
        // public void SwordSlash()
        // {
        //     if (OnSwordSlash != null)
        //     {
        //         OnSwordSlash();
        //     }
        // }

        #endregion

    }
}