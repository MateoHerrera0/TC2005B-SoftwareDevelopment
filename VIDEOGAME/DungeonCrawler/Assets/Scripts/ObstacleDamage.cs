/*
Code used for obstacle damage when main character touches it

Ana Paula Katsuda, Mateo Herrera & Gerardo Gutiérrez
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDamage : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // If obstacle touches main character
        if(other.gameObject.tag == "Main Character")
        {
            // Substract health points from Main Character
            other.GetComponentInChildren<HealthBar>().hp -= 30;
        }
    }
}
