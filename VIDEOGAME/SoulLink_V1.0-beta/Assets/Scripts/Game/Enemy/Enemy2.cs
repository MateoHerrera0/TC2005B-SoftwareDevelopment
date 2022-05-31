/*
Code used for an enemy that follows the main character

Ana Paula Katsuda, Mateo Herrera & Gerardo Gutiérrez
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    // Define movement speed
    [SerializeField] private float moveSpeed;
    // Enemy's sprite renderer
    private SpriteRenderer sprite;
    // Get main character object to extract health points info
    private GameObject mainCharacter; 
    // Transform from main character
    private Transform mainCharacterTransform;
    // Start is called before the first frame update
    void Start()
    {
        // Get enemy's sprite renderer
        sprite = GetComponent<SpriteRenderer>();
        // Get main character object
        mainCharacter = GameObject.FindGameObjectWithTag("Player");
        // Get main character's transform through tag
        mainCharacterTransform = mainCharacter.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        // Follow main character
        transform.position = Vector2.MoveTowards(transform.position, mainCharacterTransform.position, moveSpeed * 2 * Time.deltaTime);
        // if the main character's position in x is greater than enemy's x 
        if(mainCharacterTransform.position.x >= transform.position.x)
            // Set sprite to look to right
            sprite.flipX = false; 
        // if the main character's position in x is lower than enemy's x 
        if(mainCharacterTransform.position.x < transform.position.x)
            // Set sprite to look to left
            sprite.flipX = true;
        // If enemy is really close to player
        if (Vector2.Distance(transform.position, mainCharacterTransform.position) <= 0.1f)
        {
            // Access health points and reduce by 3
            mainCharacter.GetComponentInChildren<HealthBar>().hp -= 3;
        }
    }
}
