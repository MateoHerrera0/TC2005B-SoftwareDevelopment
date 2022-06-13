/*
Code used for obstacle damage while player is touching it

Ana Paula Katsuda, Mateo Herrera & Gerardo Gutiérrez
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDamage : MonoBehaviour
{
    // Get main character object to extract health points info
    private GameObject mainCharacter; 
    // Main Character transform
    private Transform mainCharacterTransform;
    // Start is called before the first frame update
    void Start()
    {
        // Get main character
        mainCharacter = GameObject.FindGameObjectWithTag("Player");
        // Get it's transform component
        mainCharacterTransform = mainCharacter.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        // If main character and obstacle are really close
        if (Vector2.Distance(transform.position, mainCharacterTransform.position) <= 0.7f)
        {
            // Access health points and reduce by 1
            mainCharacter.GetComponentInChildren<HealthBar>().hp -= 1;
            mainCharacter.GetComponent<PlayerController>().DamageEffect = true;
        }
    }
}