/*
Code used to make player move while including animation settings

Ana Paula Katsuda, Mateo Herrera & Gerardo Gutiérrez
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float mainCharacterSpeed = 1; 
    // Camera
    public Camera theCam; 
    // Movement vector
    Vector3 movement; 
    // Animate main character with animator
    public Animator animator; 

    void Start()
    {
        // Set camera to Main camera
        theCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // movement getting horizontal and vertical axis 
        movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);

        // Setting animator values with movement
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Magnitude", movement.magnitude);

        // Change position 
        transform.position = transform.position + movement * Time.deltaTime * mainCharacterSpeed;

        // Get mouseposition
        Vector3 mouse = Input.mousePosition; 
        // Transform position
        Vector3 screenPoint = theCam.WorldToScreenPoint(transform.localPosition); 
    }

    // Function to freeze movement
    void freeze()
    {
        // Set everything to 0
        animator.SetFloat("Horizontal", 0.0f);
        animator.SetFloat("Vertical", 0.0f);
        animator.SetFloat("Magnitude", 0.0f);
    }
}
