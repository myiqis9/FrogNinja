using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MiniMenus : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject deathMenu;
    [SerializeField] private GameObject finishMenu;
    [SerializeField] private Text FSTextDeath;
    [SerializeField] private Text FSTextFinish;


    private string preFinalScore = "Final Score: ";
    public string finalScore = "";

    private AsyncOperation async;
    private int currentSceneIndex;
    bool gamePaused;

    // Start is called before the first frame update
    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        gamePaused = false;

        pauseMenu.SetActive(false);
        deathMenu.SetActive(false);
        finishMenu.SetActive(false);
    }

    public void PauseGame()
    {
        Cursor.visible = true;
        Time.timeScale = 0f;
        gamePaused = true;
        pauseMenu.SetActive(true);
    }

    public void GameOver()
    {
        Cursor.visible = true;
        Time.timeScale = 0f;
        gamePaused = true;
        deathMenu.SetActive(true);
        FSTextDeath.text = preFinalScore + finalScore;
    }

    public void GameFinish()
    {
        Cursor.visible = true;
        Time.timeScale = 0f;
        gamePaused = true;
        finishMenu.SetActive(true);
        FSTextFinish.text = preFinalScore + finalScore;
    }

    public void Retry()
    {
        gamePaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void Resume()
    {
        Cursor.visible = true;
        Time.timeScale = 1f;
        gamePaused = false;
        pauseMenu.SetActive(false);
    }
    
    public void Continue()
    {
        gamePaused = false;
        Time.timeScale = 1f;
        Scene currentScene = SceneManager.GetActiveScene();
        async = SceneManager.LoadSceneAsync(currentScene.buildIndex + 1);
    }

    public void MainMenu()
    {
        gamePaused = false;
        SceneManager.LoadScene(0);
    }

    public void Save()
    {
        PlayerPrefs.Save();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused)
                Resume();
            else
                PauseGame();
        }
    }
}
