using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    public Vector2 direction = Vector2.right;
    public float speed  = 1f;
    public int size = 1;

    private Vector3 leftEdge;
    private Vector3 rightEdge;

    public void SwitchToGameBuilder()
    {
        SceneManager.LoadScene("GameBuiler");
    }

    public void SwitchToLevelSelector()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    private void Start() {
        leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);
    }

    private void Update() {
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
}
