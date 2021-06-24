using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour 
{
    public delegate void LoadingProgressHandler(float progress);
    public LoadingProgressHandler OnUpdateProgress;
    public int currentScene;

    private void Start() 
    {
        SetupDelegates();
    }

    private void OnDestroy() 
    {
        RemoveDelegates();
    }

    private void SetupDelegates()
    {
        InGameMenus.OnSetScene += SetScene;
        InGameMenus.OnRestartScene += RestartScene;
        MainMenu.OnSetScene += SetScene;
    }
    private void RemoveDelegates()
    {
        InGameMenus.OnSetScene -= SetScene;
        InGameMenus.OnRestartScene -= RestartScene;
        MainMenu.OnSetScene -= SetScene;
    }

    private void SetScene(string scene)
    {
        StartCoroutine(LoadASync(scene));
    }
    
    private IEnumerator LoadASync(string scene)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);

        while(!operation.isDone)
        {
            float loadingProgress = Mathf.Clamp01(operation.progress / 0.9f);
            OnUpdateProgress?.Invoke(loadingProgress);
           
            yield return null;
        }
    }

    private void RestartScene(string scene)
    {
        SetScene(scene);
    }
}