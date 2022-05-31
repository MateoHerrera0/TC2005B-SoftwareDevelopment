/*
Code used to define the mechanics of the arrow = throw it and call it back
And to define the arrow's damage to destruct enemies

Ana Paula Katsuda, Mateo Herrera & Gerardo Gutiérrez
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMechanic : MonoBehaviour
{
    // Determine the arrow speed
    [SerializeField] private float arrowSpeed; 
    // Mouse position that will be the target position
    private Vector3 mouseTargetPos;
    // Control click information
    private bool isClicked;
    // Get transform from the MainCharacter
    private Transform mainCharacterTrans;
    // Define if arrow is allowed to come back
    private bool canReturn;
    // Used to call function that returns arrow
    private bool returnArrow;
    // Define if arrow can damage enemies (no damage if arrow is just still)
    private bool canDamage;
    // Allow target visualization through crosshair
    public GameObject crosshairs;
    // Indicate the position of the mouse (target)
    private Vector3 target; 
    [SerializeField] private GameObject healthFlask; 


    // Start is called before the first frame update
    void Start()
    {
        // Obtain the Player through its tag
        mainCharacterTrans = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    // Update is called once per frame
    void Update()
    {
        // Obtain position through mouse movement
        target = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z)); 
        // Move crosshairs according to target
        crosshairs.transform.position = new Vector2(target.x , target.y);
        
        // If  mouse is clicked and clicked information is false
        if (Input.GetMouseButtonDown(0) && isClicked == false)
        {
            // Update click information
            isClicked = true;
            // Save the target position in mouseTargetPos
            mouseTargetPos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                                         Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
        } 
        // If clicked information is true
        if(isClicked)
        {
            // Call ThrowArrow function to throw the arrow
            ThrowArrow();
        }

        // If the distance between the arrow and target position is small
        if(Vector2.Distance(transform.position, mouseTargetPos) <= 0.01f)
        {
            // Indicate that arrow can now return
            canReturn = true;
            // Arrow can't damage while it's not moving
            canDamage = false;
        }

        // If mouse clicked and can return is true
        if(Input.GetMouseButtonDown(0) && canReturn)
        {
            // Activate posibility of arrow return
            returnArrow = true;
            // Indicate that arrow can damage again
            canDamage = true;
        }

        // if returnArrow is true
        if(returnArrow)
        {
            // Call BackArrow function to make it return
            BackArrow();
        }

        // If distance between arrow and Player is small
        if (Vector2.Distance(transform.position, mainCharacterTrans.position) <= 0.01f)
        {
            // Can't return as it has already returned
            canReturn = false;
            returnArrow = false;
            // Reset click to false
            isClicked = false;
            // Can't damage while being with the Player
            canDamage = false;
        }
    }

    // Function to throw arrow
    private void ThrowArrow()
    {
        // Get the difference between the target position and the Player
        Vector3 difference = mouseTargetPos - mainCharacterTrans.transform.position;
        // Set the rotation value using Mathf
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        // Make arrow move towards mouse target at arrowSpeed
        transform.position = Vector2.MoveTowards(transform.position, mouseTargetPos, arrowSpeed * Time.deltaTime);
        // Set rotation to point to mouse target using rotationZ
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        // Allow Damage
        canDamage = true;
        // Detach arrow from Player
        transform.SetParent(null);
    }

    // Function to call arrow back
    private void BackArrow()
    {
        // Get the distance between the Player and the arrow
        Vector3 difference = mainCharacterTrans.transform.position - transform.position;
        // Set rotation to point to Player with Mathf
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        // Move arrow towards player
        transform.position = Vector2.MoveTowards(transform.position, mainCharacterTrans.position, arrowSpeed * 2 * Time.deltaTime);
        // Attach arrow to Player 
        transform.SetParent(mainCharacterTrans);
        // Set rotation to point to Player
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
    }

    // Set arrow collisions to damage enemies
    private void OnTriggerEnter2D(Collider2D other)
    {
        // If arrow touches enemy and is allowed to damage
        if(other.gameObject.tag == "Enemy" && canDamage)
        {
            // Substract points from the enemies health points
            // Using get component in children to access HealthBar
            other.GetComponentInChildren<HealthBar>().hp -= 15;
            // If the health points are equal or lower to 0
            if(other.GetComponentInChildren<HealthBar>().hp <= 0)
            {
                // Destroy enemy
                Instantiate(healthFlask, other.transform.position, Quaternion.identity);
                RoomController.instance.StartCoroutine(RoomController.instance.RoomCoroutine());
                Destroy(other.gameObject);
            }
        }
    }
}
