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
    // Transform from main character
    private Transform mainCharacterTrans;
    // Start is called before the first frame update
    void Start()
    {
        // Get enemy's sprite renderer
        sprite = GetComponent<SpriteRenderer>();
        // Get main character's transform through tag
        mainCharacterTrans = GameObject.FindGameObjectWithTag("Main Character").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        // Follow main character
        transform.position = Vector2.MoveTowards(transform.position, mainCharacterTrans.position, moveSpeed * 2 * Time.deltaTime);
        // if the main character's position in x is greater than enemy's x 
        if(mainCharacterTrans.position.x >= transform.position.x)
            // Set sprite to look to right
            sprite.flipX = false; 
        // if the main character's position in x is lower than enemy's x 
        if(mainCharacterTrans.position.x < transform.position.x)
            // Set sprite to look to left
            sprite.flipX = true;
        // If enemy is really close to player
    }

    // If enemy touches main character
     private void OnTriggerEnter2D(Collider2D other)
    {
        // If enemy touches main character
        if(other.gameObject.tag == "Main Character")
        {
            // Substract health points from Main Character
            other.GetComponentInChildren<HealthBar>().hp -= 30;
        }
    }
}
