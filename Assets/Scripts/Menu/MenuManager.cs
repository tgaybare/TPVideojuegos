using UnityEngine;


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

    public void BackToTitle()
    {
        UnitySceneManager.instance.LoadTitleScreen();
    }
}
