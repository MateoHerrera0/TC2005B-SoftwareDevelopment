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
        // set target to Player through tag
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        // Call selfRotation function
        SelfRotation();
        // Move projectile towards Player
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

    // Collision with Player
    private void OnTriggerEnter2D(Collider2D other)
    {
        // If projectile touches Player
        if(other.gameObject.tag == "Player")
        {
            //Allow damage effect
            other.GetComponent<DamageEffect>().effect = true;
            // Substract health points from Player
            other.GetComponentInChildren<HealthBar>().hp -= 10;
            // Destroy projectile
            Destroy(gameObject);
        }
    }
}
