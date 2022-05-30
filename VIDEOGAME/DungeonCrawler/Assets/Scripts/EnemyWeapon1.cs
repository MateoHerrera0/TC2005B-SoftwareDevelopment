/*
Code used to define the behavior of a rotating weapon
And to define the damage of such weapon

Ana Paula Katsuda, Mateo Herrera & Gerardo Gutiérrez
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon1 : MonoBehaviour
{
    // Projectile's rotation speed
    [SerializeField] private float rotateSpeed;
    // Allow rotation
    public bool isRotating = true;
    // Target (where to move projectile)
    private Transform target;
    // Movement speed
    [SerializeField] private float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        // set target to main character through tag
        target = GameObject.FindGameObjectWithTag("Main Character").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        // Call selfRotation function
        SelfRotation();
        // Move projectile towards main character
        transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
    }

    // Rotation function
    private void SelfRotation()
    {
        // If allowed, change rotation values 
        if(isRotating)
        {
            transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
        }
        // If not allowed, stop rotation
        else
        {
            transform.Rotate(0, 0, 0);
        }
    }

    // Collision with main character
    private void OnTriggerEnter2D(Collider2D other)
    {
        // If projectile touches main character
        if(other.gameObject.tag == "Main Character")
        {
            // Substract health points from Main Character
            other.GetComponentInChildren<HealthBar>().hp -= 10;
            // Destroy projectile
            Destroy(gameObject);
        }
    }
}
