/*
Code used to make player move while including animation settings, its a variant
of player move. Might be deleted or replaced.

Ana Paula Katsuda, Mateo Herrera & Gerardo Gutiérrez
*/

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Player speed
    public float speed;
    // Speed to add when impulse
    public float impulseSpeed;
    // Animation
    [SerializeField] Animator anim;
    // Player's rigidBody
    Rigidbody2D rb2d;
    // Player's sprite renderer
    SpriteRenderer rendr;
    // Time counter
    private float timer; 
    // Bool to allow dash
    private bool dash; 
    // Maximum duraton for dash/dodge
    public float dashDuration; 
    // Save last player direction
    private Vector2 lastDirection;
    // Trail effect
    public GameObject trail; 
    // Player points
    public float totalPoints; 

    // Start is called before the first frame update
    void Start()
    {
        // Start with 0 points
        totalPoints = 0;
        // Get RigidBody
        rb2d = GetComponent<Rigidbody2D>();
        // Get sprite renderer
        rendr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    { 
        Debug.Log(totalPoints);
        // Get vertical input axis
        float vertical = Input.GetAxis("Vertical");
        // get horizontal input axis
        float horizontal = Input.GetAxis("Horizontal");
        
        // Set in animation
        anim.SetFloat("Horizontal", horizontal);

        // Movement for each frame depending on input and string
        Vector2 saveMovement= new Vector2(horizontal*speed, vertical*speed);

        // If player is moving and dash is false
        if((vertical != 0 || horizontal != 0) && !dash)
            // Save as last direction for player movement
            lastDirection = new Vector2(horizontal*speed, vertical*speed);
        // If horizontal is negative
        if (horizontal < 0)
        {
            // Sprite faces to left
            rendr.flipX = true;
        } 
        // If horizontal is positive or 0
        else
        {
            // Sprite faces to right
            rendr.flipX = false;
        }
        
        // Condition to allow dash: if space clicked
        if(Input.GetKeyDown("space"))
        {
            // Allow dash
            dash = true; 
            // Start timer
            timer = 0.0f; 
            // Activate trail effect
            trail.SetActive(true);
        }
        // If dash is allowed
        if (dash)
        {
            // Timer will increase 
            timer += Time.deltaTime;
            // If timer gets to the maximum dash duration
            if(timer > dashDuration)
            {
                // Stop allowing dash
                dash = false;
                // Stop trail effect
                trail.SetActive(false); 
            }
            // Increase velocity by impulse speed
            rb2d.velocity = lastDirection * impulseSpeed;
        }
        // If dash is false
        else
        {
            // Move as usual (normal player velocity)
            rb2d.velocity = saveMovement;
        }
    }
}
