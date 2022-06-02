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

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
            {
                Resume();
            }
            else
            {
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

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f; 
        isPaused = false;  
    }
    void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f; 
        isPaused = true;   
    }

    public void Quit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}