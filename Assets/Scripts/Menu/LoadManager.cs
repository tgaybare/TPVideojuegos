using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadManager : MonoBehaviour
{
    [SerializeField] private Image _progressFill;
    [SerializeField] private Text _progressText;

    private void Start()
    {
        StartCoroutine(LoadWithProgressBar());
    }

    IEnumerator LoadWithProgressBar() {
        AsyncOperation operation = UnitySceneManager.instance.LoadGameScreenAsync();
        operation.allowSceneActivation = false;

        float progress = 0;
        
        while (!operation.isDone)
        {
            progress = operation.progress;
            _progressFill.fillAmount = progress;
            _progressText.text = $"{(int)(progress * 100)}%";

            if (progress >= 0.9f)
            {
                _progressFill.fillAmount = 1;
                _progressText.text = "Press SPACE to continue";

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    operation.allowSceneActivation = true;
                }
            }

            yield return null;
        }
    }
}