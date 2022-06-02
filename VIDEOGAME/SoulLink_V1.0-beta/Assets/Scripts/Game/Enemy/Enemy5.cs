using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy5 : MonoBehaviour
{
    // Determine attack range (distance)
    [SerializeField] private float attackRange;
    // Enemy's projectile
    public GameObject projectile;
    // Projectile's target
    private Transform projectileTarget;
    // Timer to allow shots
    private float timer = 0.0f;
    // Time in between shots
    [SerializeField] private float maxTime;

    private void Start()
    {
        // The projectile target will be the Player
        projectileTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        // Increase timer in each frame
        timer += Time.deltaTime;
        // If distance between player and enemy is higher than attackRange
        if(Vector2.Distance(transform.position, projectileTarget.position) <= attackRange)
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

    // Function to shoot projectile 
    public void Shot()
    {
        // Create projectile 
        Instantiate(projectile, transform.position, Quaternion.identity);
    }
}
