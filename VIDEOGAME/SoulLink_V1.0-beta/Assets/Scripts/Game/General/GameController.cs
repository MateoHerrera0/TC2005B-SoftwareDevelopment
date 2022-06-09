/*
Code used to control general aspects of the game, like if it has ended or not.

Ana Paula Katsuda, Mateo Herrera & Gerardo Gutiérrez
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameController : MonoBehaviour
{

    public static GameController instance;
    public bool endRoomReached = false;
    public bool isTest;
    public GameObject gameTestWinUi;
    public GameObject player;
    public GameBuilderController editor;
    public GameStatistics statistics;
    LevelSelectController publisher;
    GameBuilderStatistics builderStatistics;

    // Determine if game is paused
    public static bool isPaused = false;
    bool bossPlaced = true;
    bool justOnce;
    // Pause menu object
    public GameObject pauseMenu; 

    // Timer variables
    // Variable to enable timer
    private bool activeTimer = true;
    // Timer time
    private float currentTime; 
    // Points for time
    private float timePoints;
    // Timer text
    public Text timeText;  
    public GameObject finishMenu;
    public Text finalTime;
    public Text finalPoints; 
    public int playerFinalPoints;

    // Game Over Variables
    // Game over menu object game mode
    public GameObject GameOverMenu; 
    // Game over menu object builder mode
    public GameObject BuilderOverMenu; 
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    private void Start() {
        if (isTest)
        {
            editor = GameObject.FindGameObjectWithTag("GameBuilderController").GetComponent<GameBuilderController>();
            publisher = this.GetComponent<LevelSelectController>();
            builderStatistics = editor.GetComponent<GameBuilderStatistics>();
            bossPlaced = editor.bossPlaced;
        }
        // Start timer in 0
        currentTime = 0;
        Time.timeScale = 1; 
        justOnce = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (endRoomReached && bossPlaced && GameObject.FindGameObjectWithTag("Boss") == null)
        {
            endGame();
        }
        if(!isTest)
        {
            // When escape key is pressed
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                //Debug.Log(isPaused);

                // If game is paused
                if(isPaused)
                {
                    // Resume game
                    Resume();
                }
                // If game is not paused
                else
                {
                    // Pause Game
                    Pause();
                }
            }
            // If timer allowed
            if(activeTimer)
            {
                // Update timer
                currentTime += Time.deltaTime;
            }
            // If time is bigger than 60 secs
            if(currentTime >= 60)
            {
                // Specify seconds
                TimeSpan time = TimeSpan.FromSeconds(currentTime);
                // Show time in text (structure: mins:secs:ms)
                timeText.text = time.ToString(@"mm\:ss\:ff");
            }
            // If time is smaller than 60 secs
            else
            {
                // Specify seconds
                TimeSpan time = TimeSpan.FromSeconds(currentTime);
                // Show time in text (structure: mins:secs:ms)
                timeText.text = time.ToString(@"ss\:ff");
            }
            // If player lost their whole hp
            if(player.GetComponentInChildren<HealthBar>().hp <= 0)
            {
                GameOver();
            }
        }
        else
        {
            // If player lost their whole hp
            if(player.GetComponentInChildren<HealthBar>().hp <= 0)
            {
                TestOver();
            }
        }
        
    }

    // Function used when player wins game - finish
    void endGame()
    {
        // Deactivate player
        player.SetActive(false);
        // If currently on test
        if (isTest)
        {
            // Set game tes menu UI
            gameTestWinUi.SetActive(true);
        }
        // If currently on game mode
        else
        {
            // Show timer
            timeText.enabled = false; 
            // Resume timer
            activeTimer = false;
            // Activate finish menu
            finishMenu.SetActive(true);
            // Calculate points from time
            timePoints = 3600 - currentTime; 
            // Ponts from time + points from enemies
            playerFinalPoints = Mathf.RoundToInt(player.GetComponent<PlayerController>().totalPoints + timePoints);
            // Specify seconds
            TimeSpan time = TimeSpan.FromSeconds(currentTime);
            // Place time text
            finalTime.text = time.ToString(@"mm\:ss\:ff");
            // Place score text
            finalPoints.text = playerFinalPoints.ToString();
            if (justOnce)
            {
                statistics.UpdateGameStatistics(playerFinalPoints, currentTime);   
            }
            justOnce = false;
        }
    }

    public void CancelPublish()
    {
        editor.ToggleGameBuilder();
    }

    public void Publish()
    {
        publisher.InsertNewLevel();
        builderStatistics.UpdateStatistics();
        editor.ReturnToMenu();
    }

    // Function to resume game
    public void Resume()
    {
        // Deactivate pause menu
        pauseMenu.SetActive(false);
        // Time runs as normal
        Time.timeScale = 1f; 
        // Specify that it is not paused
        isPaused = false;  
        // Show timer
        timeText.enabled = true; 
        // Resume timer
        activeTimer = true; 
    }

    // Function to pause game
    void Pause()
    {
        // Activate pause meny
        pauseMenu.SetActive(true);
        // Time runs as if application is paused
        Time.timeScale = 0f; 
        // Specify that it is paused
        isPaused = true;   
        // Stop timer
        activeTimer = false; 
        // Stop showing timer
        timeText.enabled = false; 
    }
    public void Quit()
    {
        // Time runs as normal
        Time.timeScale = 1f; 
        // Load main menu
        SceneManager.LoadScene("MainMenu");
    }
    // Function to quit and go to main meny
    public void QuitBuilder()
    {
        // Time runs as normal
        Time.timeScale = 1f; 
        // Load main menu
        editor.ReturnToMenu();
    }
    // Function for game over in game mode
    void GameOver()
    {
        // Activate gameover menu
        GameOverMenu.SetActive(true);
        // Time runs as if application is paused
        Time.timeScale = 0f; 
        // Stop timer
        activeTimer = false; 
        // Stop showing timer
        timeText.enabled = false; 
    }
    // Function for game over in test mode
    void TestOver()
    {
        // Activate gameover menu
        BuilderOverMenu.SetActive(true);
        // Time runs as if application is paused
        Time.timeScale = 0f; 
        // Stop timer
        activeTimer = false; 
        // Stop showing timer
        timeText.enabled = false; 
    }
    // Function to play again
    public void PlayAgain()
    {
        // Time runs as normal
        Time.timeScale = 1f;
        // Load main menu
        SceneManager.LoadScene("Level");
    }
    public void ReturnBuilder()
    {
        // Time runs as normal
        Time.timeScale = 1f;
        editor.ToggleGameBuilder();
        player.GetComponentInChildren<HealthBar>().hp = 120;
    }
}