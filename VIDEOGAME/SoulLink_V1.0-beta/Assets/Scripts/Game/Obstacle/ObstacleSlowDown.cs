/*
Code used to slow down main character when it touches obstacle

Ana Paula Katsuda, Mateo Herrera & Gerardo Gutiérrez
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSlowDown : MonoBehaviour
{
    // New speed variable
    private float newSpeed = 0.5f; 
    private void OnTriggerEnter2D(Collider2D other)
    {
        // If obstacle touches main character
        if(other.gameObject.tag == "Player")
        {
            // Reduce speed
            other.GetComponent<PlayerController>().speed *= newSpeed;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        // If obstacle touches main character
        if(other.gameObject.tag == "Player")
        {
            // Turn speed to normal
            other.GetComponent<PlayerController>().speed /= newSpeed;
        }
    }
}
