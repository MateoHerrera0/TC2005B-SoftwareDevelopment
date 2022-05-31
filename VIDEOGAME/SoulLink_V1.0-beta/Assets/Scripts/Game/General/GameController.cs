// Script that will contain and updpate general game stats that will be used elsewhere;
// Mateo Herrera Lavalle
// 7-04-2022

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
}