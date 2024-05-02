using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } // GameManager instance
    public GameObject Panel1;
    public GameObject Panel2;
    public GameObject winPanel;
    public GameObject losePanel;
    public GameObject bgMusic;

    private int expectedScoreToWin = 100; // Expected score required to win
    public Text scoreText;
    public TMP_Text finalScoreText1;
    public TMP_Text finalScoreText2;
    private int currentScore; // Current score
    
    

    void Awake()
    {
        currentScore = 0;
        // Ensure there is only one instance of GameManager
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(bgMusic);
    }

    // Function to update the score
    public void UpdateScore()
    {
        
        currentScore += 10;
        scoreText.text = "Score: " + currentScore.ToString();
    }

    public void PlayBtn()
    {
        Panel1.SetActive(true);
    }

    public void NextBtn1()
    {
        Panel1.SetActive(false);
        Panel2.SetActive(true);
    }

    public void NextBtn2()
    {
        Panel1.SetActive(false);
        Panel2.SetActive(false);
        SceneManager.LoadSceneAsync(1);
    }
    public void BackBtn1()
    {
        Panel1.SetActive(false);
    }

    public void BackBtn2()
    {
        Panel1.SetActive(true);
        Panel2.SetActive(false);
    }

    public void SkipBtn()
    {
        Panel1.SetActive(false);
        SceneManager.LoadSceneAsync(1);
    }

    public void HomeBtn()
    {
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        SceneManager.LoadSceneAsync(0);
    }

    public void RetryBtn()
    {
        winPanel.SetActive(false);
        losePanel.SetActive(false);
    }
    

    //Function to check the game result
    public void CheckGameResult()
    {
        if (currentScore >= expectedScoreToWin)
        {
            // Display "You Won" screen
            finalScoreText1.text = "Your Score: " + currentScore.ToString();
            winPanel.SetActive(true);
            AudioHandler.Instance.WinSfx();
        }
        else
        {
            // Display "You Lose" screen
            finalScoreText2.text = "Your Score: " + currentScore.ToString();
            losePanel.SetActive(true);
            AudioHandler.Instance.LoseSfx();
        }
    }
}
