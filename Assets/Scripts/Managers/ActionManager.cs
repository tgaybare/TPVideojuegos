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
        }

        #region GAME_MANAGER_ACTIONS
        
        public event Action<bool> OnGameOver;

        public void ActionGameOver(bool isVictory)
        {
            if (OnGameOver != null)
            {
                OnGameOver(isVictory);
                Invoke(nameof(LoadMenuScreen),6f);
            }
        }

        private void LoadMenuScreen() => UnitySceneManager.instance.Load_MenuScreen();

        #endregion GAME_MANAGER_ACTIONS
        
        #region HUD_UI_ACTIONS

        public event Action<float,float> OnCharacterLifeChange;
        public event Action<int, int> OnWeaponChange;
        
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

        // ejemplo muere un enemigo

        #endregion

    }
}