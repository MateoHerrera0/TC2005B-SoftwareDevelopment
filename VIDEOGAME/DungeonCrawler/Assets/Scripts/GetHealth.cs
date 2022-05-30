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
        // Obtain the main character through its tag
        mainCharacterTrans = GameObject.FindGameObjectWithTag("Main Character").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        // If the distance between the flask and the main character is small
         if(Vector2.Distance(transform.position, mainCharacterTrans.position) <= 0.1f)
        {
            // Move flask towards main character
            transform.position = Vector2.MoveTowards(transform.position, mainCharacterTrans.position, flaskSpeed* Time.deltaTime);
        }
    }  

    // Flask collision
    private void OnTriggerEnter2D(Collider2D other)
    {
        // If flask touches main character
        if(other.gameObject.tag == "Main Character")
        {
            // If Main character doesn't have all their points
            if(other.GetComponentInChildren<HealthBar>().hp <= other.GetComponentInChildren<HealthBar>().maxHp - 15)
            {
                // Add points to the main character's health points
                other.GetComponentInChildren<HealthBar>().hp += 15;
                // Destroy flask
                Destroy(gameObject);
            }
        }
    }
}
