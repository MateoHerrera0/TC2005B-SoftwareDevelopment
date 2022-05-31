/*
Code used to gain health through obtaining flasks

Ana Paula Katsuda, Mateo Herrera & Gerardo Gutiérrez
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHealth : MonoBehaviour
{
    // Get transform from the MainCharacter
    private Transform mainCharacterTrans;
    // Get the speed of the flask
    [SerializeField] private float flaskSpeed; 

    // Start is called before the first frame update
    void Start()
    {
        // Obtain the Player through its tag
        mainCharacterTrans = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        // If the distance between the flask and the Player is small
         if(Vector2.Distance(transform.position, mainCharacterTrans.position) <= 0.1f)
        {
            // Move flask towards Player
            transform.position = Vector2.MoveTowards(transform.position, mainCharacterTrans.position, flaskSpeed* Time.deltaTime);
        }
    }  

    // Flask collision
    private void OnTriggerEnter2D(Collider2D other)
    {
        // If flask touches Player
        if(other.gameObject.tag == "Player")
        {
            // If Player doesn't have all their points
            if(other.GetComponentInChildren<HealthBar>().hp <= other.GetComponentInChildren<HealthBar>().maxHp)
            {
                if (other.GetComponentInChildren<HealthBar>().maxHp - other.GetComponentInChildren<HealthBar>().hp > 15)
                {
                    // Add points to the Player's health points
                    other.GetComponentInChildren<HealthBar>().hp += 15;
                } else
                {
                    other.GetComponentInChildren<HealthBar>().hp = other.GetComponentInChildren<HealthBar>().maxHp;
                }
                // Destroy flask
                Destroy(gameObject);
            }
        }
    }
}
