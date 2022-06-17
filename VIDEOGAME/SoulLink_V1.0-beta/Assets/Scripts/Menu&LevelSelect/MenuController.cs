/*
Code used to move background in main menu and handles scene management in it
as well.

Ana Paula Katsuda, Mateo Herrera & Gerardo Gutiérrez
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Default unity vector of right pos.
    public Vector2 direction = Vector2.right;
    // Default unity vector of up pos.
    Vector2 direction2 = Vector2.up;
    // Public speed variable of parallax effect.
    public float speed  = 1f;
    // Sixe of sprite that needs to be moved
    public int size = 1;
    // Variable that stores logout panel
    public GameObject logoutPanel;
    // Variable that checks if logout menu si currently open
    bool currentlyOpenMenu = false;

    // Vector that stores position of left edge of camera
    private Vector3 leftEdge;
    // Vector that stores position of right edge of camera
    private Vector3 rightEdge;
    // Vector that stores position of up edge of camera
    private Vector3 upEdge;
    // Vector that stores position of bottom edge of camera
    private Vector3 bottomEdge;
    // Variable that checks if objects need to move up
    public bool up;

    // Variable that stores player statistics
    [SerializeField] PlayerStatistics playerStatistics;

    // Function that switches to game builder
    public void SwitchToGameBuilder()
    {
        SceneManager.LoadScene("GameBuiler");
    }

    // Function that switches to login
    public void SwitchToLogin()
    {
        SceneManager.LoadScene("UserLogin");
    }

    // Function that switches to level selectore
    public void SwitchToLevelSelector()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    // Function that switches back to main menu
    public void SwitchToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Function that toggles the logout menu
    public void ToggleLogoutMenu()
    {
        if (!currentlyOpenMenu)
        {
            logoutPanel.SetActive(true);
            currentlyOpenMenu = true;
        } else
        {
            logoutPanel.SetActive(false);
            currentlyOpenMenu = false;
        }
    }

    // Function that logs out user, and it switches to start sceen
    public void Logout()
    {
        playerStatistics.UpdatePlayerStatistics(false);
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("StartScreem");
    }

    // On start all camera edges are captured and timescale starts
    private void Start() {
        leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);
        upEdge = Camera.main.ViewportToWorldPoint(Vector3.up);
        bottomEdge = Camera.main.ViewportToWorldPoint(Vector3.down);
        Time.timeScale = 1;
    }

    // In update background game objects move to set speed and return to 
    // original position when camera edge is crossed.
    private void Update() {
        if (!up)
        {
            if(direction.x > 0 && (transform.position.x - size) > rightEdge.x)
            {
                transform.position = new Vector3(leftEdge.x - size, transform.position.y, transform.position.z);
            }
            else if(direction.x < 0 && (transform.position.x + size) < leftEdge.x)
            {
                transform.position = new Vector3(rightEdge.x + size, transform.position.y, transform.position.z);
            }
            else
            {
                transform.Translate(direction * speed * Time.deltaTime);
            }
        }
        else
        {
            if (transform.position.y - size > bottomEdge.y)
            {
                transform.position = new Vector3(transform.position.x, upEdge.y - size, transform.position.z);
            }
            else
            {
                transform.Translate(Vector3.up * speed * Time.deltaTime, Space.World);
            }
        }
    }
}
