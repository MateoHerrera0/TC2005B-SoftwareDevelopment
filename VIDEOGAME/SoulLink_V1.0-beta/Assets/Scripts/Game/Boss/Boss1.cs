/*
Code used for a boss that appears and disappears in n time 
Boss follows player

Ana Paula Katsuda, Mateo Herrera & Gerardo Gutiérrez
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : MonoBehaviour
{
    // Allow Boss to disappear
    public bool disappear;
    // appear/disappear time count
    public float timer; 
    // maximum time to appear/disappear
    public float maxTime; 
    // Define movement speed
    [SerializeField] private float moveSpeed;
    // Boss' sprite renderer
    private SpriteRenderer sprite;
    // Boss' collider 
    Collider2D bossCollider; 
    // Get main character object to extract health points info
    private GameObject mainCharacter; 
    // Transform from main character
    private Transform mainCharacterTransform;
    // Healthbar Object
    Canvas showBar;

    // Start is called before the first frame update
    void Start()
    {
        // Get enemy's sprite renderer
        sprite = GetComponent<SpriteRenderer>();
        // Get main character object
        mainCharacter = GameObject.FindGameObjectWithTag("Player");
        // Get main character's transform through tag
        mainCharacterTransform = mainCharacter.GetComponent<Transform>();
        // Get Boss' collider
        bossCollider = GetComponent<Collider2D>();
        // Get canvas object 
        showBar = GetComponentInChildren<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        // Update time
        timer += Time.deltaTime;
        // If timer gets to limit and disappear is true
        if(timer > (maxTime*5.0f) && disappear)
        {
            // Disable boss' collider
            bossCollider.enabled = false; 
            // Disable boss' sprite
            sprite.enabled = false;  
            // Restart time 
            timer = 0.0f; 
            // Turn off disappear 
            disappear = false;
            // Disable boss' health bar
            showBar.enabled = false; 
        }
        // If time gets to limit and disappear is false
        if(timer > maxTime && !disappear)
        {
            // Enable boss' collider
            bossCollider.enabled = true; 
            // Enable boss' sprite
            sprite.enabled = true;  
            // Restart time
            timer = 0.0f; 
            // Turn on disappear 
            disappear = true;
            // Enable boss' health bar
            showBar.enabled = true; 
        }
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
            mainCharacter.GetComponentInChildren<HealthBar>().hp -= 5;
            //Allow damage effect
            mainCharacter.GetComponent<DamageEffect>().effect = true;
        }
    }
}
