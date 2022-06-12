/*
Code used for an enemy that goes side to side and stops to shoot player 
when they are within the attack range

Ana Paula Katsuda, Mateo Herrera & Gerardo Gutiérrez
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    // Direction of the enemy
    private float direction;
    // Determine move speed 
    [SerializeField] private float moveSpeed;
    // Determine attack range (distance)
    [SerializeField] private float attackRange;
    // Get sprite renderer to change direction of sprite
    [SerializeField] private SpriteRenderer rendr;
    // Enemy's projectile
    public GameObject projectile;
    // Projectile's target
    private Transform projectileTarget;
    // Timer to allow shots
    private float timer = 0.0f;
    // Time in between shots
    [SerializeField] private float maxTime;
    // Determine if enemy was attacked to run effect
    public bool wasAttacked; 
    // time of attacked effect
    private float attackedTime; 
    // 
    private bool colliderExit = true; 

    private void Start()
    {
        // Start direction
        direction = -1.0f;
        // The projectile target will be the Player
        projectileTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        // Increase timer in each frame
        timer += Time.deltaTime;
        // If distance between player and enemy is higher than attackRange
        if(Vector2.Distance(transform.position, projectileTarget.position) >= attackRange)
        {
            // Call patrol function to make enemy move side to side
            Patrol();
        }
        // If distance is lower than attackRange
        else
        {
            // If timer gets close to maxTime
            if(timer >= maxTime)
            {
                // Allow call to Shot function
                Shot();
                // Restart timer
                timer = 0.0f;
            }
        }
        //Debug.Log(direction);
    }

    // Function to make enemy move from a point to another
    private void Patrol()
    {
        // When direction is positive
        if(direction > 0.0f)
        {
            // Movement to right
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            // Change sprite direction (to match movement)
            rendr.flipX = false; 
        }
        // when direction is negative
        if(direction < 0.0f)
        {
            // Movement to left
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
            // Change sprite direction (to match movement)
            rendr.flipX = true; 
        }
    }



    // Function to shoot projectile 
    public void Shot()
    {
        // Create projectile 
        Instantiate(projectile, transform.position, Quaternion.identity);
    }
    // 
    // Collision with stop objects (walls and other obstacles)
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("babye");
        // If detects collision with stop object
        if(other.gameObject.tag == "Stop" && colliderExit)
        {
            // Change direction sign 
            direction *= -1.0f;
            colliderExit = false; 
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        //Debug.Log("babye");
        // If detects collision with stop object
        if(other.gameObject.tag == "Stop")
        {
            colliderExit = true; 
        }
    }
}