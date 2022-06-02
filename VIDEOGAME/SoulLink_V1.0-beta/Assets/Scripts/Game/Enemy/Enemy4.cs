using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy4 : MonoBehaviour
{
    // Direction of the enemy
    private float direction;
    // Determine move speed 
    [SerializeField] private float moveSpeed;
    // Get main character object to extract health points info
    private GameObject mainCharacter; 
    // Transform from main character
    private Transform mainCharacterTransform;

    private void Start()
    {
        // Get main character object
        mainCharacter = GameObject.FindGameObjectWithTag("Player");
        // Get main character's transform through tag
        mainCharacterTransform = mainCharacter.GetComponent<Transform>();
        // Start direction
        direction = -1.0f;
    }

    private void Update()
    {
        Patrol();
        if (Vector2.Distance(transform.position, mainCharacterTransform.position) <= 0.1f)
        {
            // Access health points and reduce by 3
            mainCharacter.GetComponentInChildren<HealthBar>().hp -= 3;
        }
    }

    // Function to make enemy move from a point to another
    private void Patrol()
    {
        // When direction is positive
        if(direction > 0.0f)
        {
            // Movement to right
            transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
        }
        // when direction is negative
        if(direction < 0.0f)
        {
            // Movement to left
            transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
        }
    }

    // Collision with stop objects (walls and other obstacles)
    private void OnTriggerEnter2D(Collider2D other)
    {
        // If detects collision with stop object
        if(other.gameObject.tag == "Stop")
        {
            // Change direction sign 
            direction *= -1.0f;
        }
    }
}
