using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2 : MonoBehaviour
{
    // Allow Boss to turn into rock
    public bool toRock;
    // appear/disappear time count
    public float timer; 
    // maximum time to appear/disappear
    public float maxTime = 0.0f; 
    // Define movement speed
    [SerializeField] private float moveSpeed;
    // Boss' sprite renderer
    private SpriteRenderer sprite;
    Animator bossAnimation;
    // Boss' collider 
    Collider2D bossCollider; 
    // Get main character object to extract health points info
    private GameObject mainCharacter; 
    // Transform from main character
    private Transform mainCharacterTransform;
    // Healthbar Object
    Canvas showBar;
    // Determine attack range (distance)
    [SerializeField] private float attackRange;
    // Enemy's projectile
    public GameObject projectile;
    // Projectile's target
    private Transform projectileTarget;

    // Start is called before the first frame update
    void Start()
    {
        // Get enemy's animator
        bossAnimation = GetComponent<Animator>();
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
        // The projectile target will be the Player
        projectileTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
   
    }

    // Update is called once per frame
    void Update()
    {
        // Increase timer in each frame
        timer += Time.deltaTime;
        // 
        if(Vector2.Distance(transform.position, projectileTarget.position) >= attackRange)
        {
            // Call patrol function to make enemy move side to side
            Patrol();
            // Enable boss' collider
            bossCollider.enabled = true; 
            // Enable boss' health bar
            showBar.enabled = true; 
            bossAnimation.enabled = true;
            sprite.color = Color.white;
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
                // Disable boss' collider
                bossCollider.enabled = false;  
                // Disable boss' health bar
                showBar.enabled = false; 
                bossAnimation.enabled = false;
                sprite.color = Color.grey;
            }
        }
        // If enemy is really close to player
        if (Vector2.Distance(transform.position, mainCharacterTransform.position) <= 0.1f)
        {
            // Access health points and reduce by 3
            mainCharacter.GetComponentInChildren<HealthBar>().hp -= 5;
        }
    }

    // Function to make enemy move from a point to another
    private void Patrol()
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
    }

    // Function to shoot projectile 
    public void Shot()
    {
        // Create projectile 
        Instantiate(projectile, transform.position, Quaternion.identity);
    }
}
