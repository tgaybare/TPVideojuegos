﻿using Managers;
using UnityEngine;

namespace Menu
{
    public class MenuManager : MonoBehaviour
    {
        public void PlayButton()
        {
            UnitySceneManager.instance.LoadLoadingScreen();
        }

        public void ExitButton()
        {
            Application.Quit();
        }
    }
}