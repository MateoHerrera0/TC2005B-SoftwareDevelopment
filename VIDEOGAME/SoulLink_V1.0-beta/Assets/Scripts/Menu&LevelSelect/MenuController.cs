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

    public Vector2 direction = Vector2.right;
    Vector2 direction2 = Vector2.up;
    public float speed  = 1f;
    public int size = 1;
    public GameObject logoutPanel;
    bool currentlyOpenMenu = false;

    private Vector3 leftEdge;
    private Vector3 rightEdge;
    private Vector3 upEdge;
    private Vector3 bottomEdge;
    public bool up;

    public void SwitchToGameBuilder()
    {
        SceneManager.LoadScene("GameBuiler");
    }

    public void SwitchToLogin()
    {
        SceneManager.LoadScene("UserLogin");
    }

    public void SwitchToLevelSelector()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void SwitchToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

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

    public void Logout()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("StartScreem");
    }

    private void Start() {
        leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);
        upEdge = Camera.main.ViewportToWorldPoint(Vector3.up);
        bottomEdge = Camera.main.ViewportToWorldPoint(Vector3.down);
        Time.timeScale = 1;
    }

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
        Debug.Log(Time.deltaTime);
    }
}
