/*
Code used for an enemy that moves from one point to another
If player enters the attack range, the enemy will stop moving 
and shoot projectiles

Ana Paula Katsuda, Mateo Herrera & Gerardo Gutiérrez
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    // Limits for enemy movement (2 points)
    public Transform point1, point2;
    // Determine which point is the target
    private Transform pointTarget;
    // Determine move speed 
    [SerializeField] private float moveSpeed;
    // Determine attack range (distance)
    [SerializeField] private float attackRange;
    // Enemy's projectile
    public GameObject projectile;
    // Projectile's target
    private Transform projectileTarget;
    // From where the projectile will be shot
    public Transform firePoint;
    // Timer to allow shots
    private float timer = 0.0f;
    // Time in between shots
    [SerializeField] private float maxTime;

    private void Start()
    {
        // The first point target will be point1
        pointTarget = point1;
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
    }

    // Function to make enemy move from a point to another
    private void Patrol()
    {
        // Move towards pointTarget at moveSpeed
        transform.position = Vector2.MoveTowards(transform.position, pointTarget.position, moveSpeed * Time.deltaTime);
        // When really close to point1
        if(Vector2.Distance(transform.position, point1.position) < 0.01f)
        {
            // Change pointTarget to point2
            pointTarget = point2;
            // Change sprite direction (to match movement)
            Vector3 localTemp = transform.localScale;
            localTemp.x *= -1;
            transform.localScale = localTemp;
        }
        // When really close to point 2
        if (Vector2.Distance(transform.position, point2.position) < 0.01f)
        {
            // Change pointTarget to point1
            pointTarget = point1;
             // Change sprite direction (to match movement)
            Vector3 localTemp = transform.localScale;
            localTemp.x *= -1;
            transform.localScale = localTemp;
        }
    }



    // Function to shoot projectile 
    public void Shot()
    {
        // Create projectile 
        Instantiate(projectile, firePoint.position, Quaternion.identity);
    }
}
