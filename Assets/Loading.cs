using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

public class Loading : MonoBehaviour
{
    private AsyncOperation async;
    [SerializeField] private Image progressBar;
    [SerializeField] private Text txtPercent;

    private bool ready = false;

    [SerializeField] private float delay = 5f;
    [SerializeField] private string loadSceneName = "";
    [SerializeField] private int loadSceneIndex = -1;

    void Start()
    {
        InitParam();
        LoadScene();
    }

    public void Activate()
    {
        ready = true;
    }

    void InitParam()
    {
        Time.timeScale = 1.0f;
        Input.ResetInputAxes();
        System.GC.Collect();
    }

    void LoadScene()
    {
        if (loadSceneIndex < 0 || loadSceneIndex > SceneManager.sceneCountInBuildSettings - 1)
        {
            Scene currentScene = SceneManager.GetActiveScene();
            async = SceneManager.LoadSceneAsync(currentScene.buildIndex + 1);
        }
        else
        {
            async = SceneManager.LoadSceneAsync(loadSceneIndex);
        }

        async.allowSceneActivation = false;
        Invoke("Activate", delay);
    }

    void Update()
    {
        if (progressBar)
            progressBar.fillAmount = async.progress + 0.1f;

        if (txtPercent)
            txtPercent.text = ((async.progress + 0.1f) * 100).ToString("00") + "%";

        if (async.progress > 0.89f && SplashScreen.isFinished && ready)
        {
            async.allowSceneActivation = true;
        }
    }
}
