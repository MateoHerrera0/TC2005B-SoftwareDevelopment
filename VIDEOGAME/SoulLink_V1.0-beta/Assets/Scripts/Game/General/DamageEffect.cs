/*
Code used to change sprite color as damage effect when a character 
within the game receives damage

Ana Paula Katsuda, Mateo Herrera & Gerardo Gutiérrez
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEffect : MonoBehaviour
{
    // Sprite renderer
    private SpriteRenderer sprite;
    // Determine effect timing
    private float timer = 0.0f;
    // Allow effect to happen
    public bool effect = false;

    void Start()
    {
        // Get sprite renderer
        sprite = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        // If effect is allowed
        if(effect)
        {
            // Increase timer
            timer += Time.deltaTime;
            // If timer is lower than 0.1 
            if(timer < 0.1f)
                // Change sprite color to red
                sprite.color = Color.red;
            // If timer bigger than 0.1
            else
            {
                // Change color to default (white)
                sprite.color = Color.white;
                // Stop allowing effect
                effect = false;
                // Restart timer
                timer = 0.0f;
            }
        }
    }
}
