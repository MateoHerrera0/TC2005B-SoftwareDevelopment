/*
Code used to control general aspects of the game, like if it has ended or not.

Ana Paula Katsuda, Mateo Herrera & Gerardo Gutiérrez
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public static GameController instance;
    public bool endRoomReached = false;
    public bool isTest;
    public GameObject gameTestWinUi;
    public GameObject player;
    public GameBuilderController editor;
    LevelSelectController publisher;

    // Determine if game is paused
    public static bool isPaused = false; 
    // 
    public GameObject pauseMenu; 
    
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
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (endRoomReached)
        {
            endGame();
        }
        // When escape key is pressed
        if(Input.GetKeyDown(KeyCode.Escape))
        {
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
    }

    void endGame()
    {
        player.SetActive(false);
        if (isTest)
        {
            gameTestWinUi.SetActive(true);
        }
    }

    public void CancelPublish()
    {
        editor.ToggleGameBuilder();
    }

    public void Publish()
    {
        publisher.InsertNewLevel();
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
    }

    // Function to quit and go to main meny
    public void Quit()
    {
        // Load main menu
        SceneManager.LoadScene("MainMenu");
    }
}