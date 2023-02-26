using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    [SerializeField] private int lives = 3;

    [SerializeField] private TMP_Text scoretxt;
    [SerializeField] private TMP_Text livestxt;
    private string preTextBest = "";
    const string preScore = "SCORE: ";
    const string preLives = "LIVES: ";

    private int score = 0;
    private int bestScore = 0;

    // variables for menu
    public bool gameOver;
   [SerializeField] private MiniMenus menus;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        Cursor.visible = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        bestScore = PlayerPrefs.GetInt("BScore");

        RefreshUI();
    }

    public void Quit()
    {
        Application.Quit();
    }

    void RefreshUI()
    {
        scoretxt.text = preTextBest + preScore + score.ToString("D5");
        livestxt.text = preLives + lives.ToString("D1");
        menus.finalScore = score.ToString();
    }

    public void AddPoints()
    {
        score += 25;
        if (score > bestScore)
        {
            preTextBest = "[!] ";
            bestScore = score;
            PlayerPrefs.SetInt("BScore", score);
        }
        PlayerPrefs.Save();
        RefreshUI();
    }

    public void LoseLife()
    {
        score -= 200;
        if(score < 0)
        {
            score = 0;
        }
        lives--;
        RefreshUI();
        CheckLife();
    }

    void CheckLife()
    {
        if (lives <= 0)
        {
            menus.GameOver();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver == true)
            menus.GameFinish();
        RefreshUI();
    }
}